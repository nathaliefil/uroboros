using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.expressions.list;
using Uroboros.syntax.variables.constants;
using Uroboros.syntax.interpretation.expressions;

namespace Uroboros.syntax.interpretation.expressions
{
    class ListableBuilder
    {
        public static IListable Build(List<Token> tokens)
        {
            // try to build Stringable
            IStringable ist = StringableBuilder.Build(tokens);
            if (!ist.IsNull())
                return (ist as IListable);

            // remove first and last bracket if it is there
            while (tokens[0].GetTokenType().Equals(TokenType.BracketOn) && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff) &&
                !Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
            {
                List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();
                tokensCopy.RemoveAt(tokens.Count - 1);
                tokensCopy.RemoveAt(0);
                tokens = tokensCopy;
            }

            // try to build 'empty list'
            if (tokens.Count == 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.Variable)
                && tokens[0].GetContent().ToLower().Equals("empty") && tokens[1].GetContent().ToLower().Equals("list"))
                return new EmptyList();

            // try to build small arrow function
            if (tokens.Where(t => t.GetTokenType().Equals(TokenType.SmallArrow)).Count() == 1)
            {
                IListable smallArrow = BuildSmallArrowFunction(tokens);
                if (!smallArrow.IsNull())
                    return smallArrow;
            }
            
            string str = tokens[0].GetContent();

            // try to build list variable reference - just one word and it is a name
            if (tokens.Count == 1 && tokens[0].GetTokenType().Equals(TokenType.Variable) && InterVariables.GetInstance().Contains(str, InterVarType.List))
                return new ListVariableRefer(str);

            // try to build list expression
            if (InterVariables.GetInstance().Contains(str, InterVarType.List))
            {
                IListable listEx = BuildListExpression(tokens, str);
                if (!listEx.IsNull())
                    return listEx;
            }

            // try to build list ternary
            if (TernaryBuilder.IsPossibleTernary(tokens))
            {
                IListable ilist = TernaryBuilder.BuildListTernary(tokens);
                if (!ilist.IsNull())
                    return ilist;
            }

            // try to build listed lists/strings: many Listables/Stringables divided by commas
            if (ContainsCommas(tokens))
            {
                IListable listed = BuildListed(tokens);
                if (!listed.IsNull())
                    return listed;
            }

            throw new SyntaxErrorException("ERROR! Unknown error in code syntax.");
        }

        private static IListable BuildListExpression(List<Token> tokens, string str)
        {
            // take second word and check if it is subcommand keyword (first, last, where...)
            if (!TokenGroups.IsSubcommandKeyword(tokens[1].GetTokenType()))
                return null;

            // build ListExpression and add subcommands to it
            ListExpression list = new ListExpression(new ListVariableRefer(str));
            tokens.RemoveAt(0);
            List<Token> currentTokens = new List<Token>();
            TokenType subcommandType = TokenType.Where;

            foreach (Token tok in tokens)
            {
                if (TokenGroups.IsSubcommandKeyword(tok.GetTokenType()))
                {
                    if (currentTokens.Count > 0)
                    {
                        list.AddSubcommand(SubcommandBuilder.Build(currentTokens, subcommandType));
                        currentTokens.Clear();
                    }
                    subcommandType = tok.GetTokenType();
                }
                else
                    currentTokens.Add(tok);
            }

            if (currentTokens.Count > 0)
                list.AddSubcommand(SubcommandBuilder.Build(currentTokens, subcommandType));
            if (currentTokens.Count == 0)
                list.AddSubcommand(SubcommandBuilder.BuildEmpty(subcommandType));

            return list;
        }

        private static IListable BuildSmallArrowFunction(List<Token> tokens)
        {
            bool unique = false;
            List<Token> leftSide = new List<Token>();
            List<Token> rightSide = new List<Token>();
            bool pastTo = false;
            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.SmallArrow))
                    pastTo = true;
                else
                {
                    if (pastTo)
                        rightSide.Add(tok);
                    else
                        leftSide.Add(tok);
                }
            }

            if (leftSide.Count == 0)
                throw new SyntaxErrorException("ERROR! Left side of Small Arrow Function is empty.");
            if (rightSide.Count == 0)
                throw new SyntaxErrorException("ERROR! Right side of Small Arrow Function is empty.");

            if (rightSide.First().GetTokenType().Equals(TokenType.Unique))
            {
                unique = true;
                rightSide.RemoveAt(0);
                if (rightSide.Count == 0)
                    throw new SyntaxErrorException("ERROR! Right side of Small Arrow Function contains only one word: unique.");
            }

            IListable ilist = ListableBuilder.Build(leftSide);
            if (ilist.IsNull())
                return null;

            IStringable istr = StringableBuilder.Build(rightSide);
            if (istr.IsNull())
                return null;

            return new SmallArrow(ilist, istr, unique);
        }

        private static IListable BuildListed(List<Token> tokens)
        {
            List<Token> currentTokens = new List<Token>();
            List<IListable> elements = new List<IListable>();
            int level = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].GetTokenType().Equals(TokenType.BracketOn))
                    level++;
                if (tokens[i].GetTokenType().Equals(TokenType.BracketOff))
                    level--;

                if (tokens[i].GetTokenType().Equals(TokenType.Comma) && level == 0)
                {
                    if (currentTokens.Count > 0)
                    {
                        IListable ilist = ListableBuilder.Build(currentTokens);
                        currentTokens.Clear();
                        if (ilist.IsNull())
                            return null;
                        else
                            elements.Add(ilist);
                    }
                }
                else
                    currentTokens.Add(tokens[i]);
            }

            if (currentTokens.Count > 0)
            {
                IListable ilist = ListableBuilder.Build(currentTokens);
                if (ilist.IsNull())
                    return null;
                else
                    elements.Add(ilist);
            }

            if (elements.Count == 0)
                return null;

            if (elements.All(e => e is IStringable))
            {
                if (elements.All(e => e is StringConstant))
                    return new ListConstant(elements.Select(e => e.ToString()).ToList());
                else
                    return new ListedStringables(elements.Select(e => e as IStringable).ToList());
            }
            else
            {
                if (elements.All(e => e is StringConstant || e is ListConstant))
                    return new ListConstant(elements.Select(e => e.ToString()).ToList());
                else
                    return new ListedListables(elements);
            }
        }

        private static bool ContainsCommas(List<Token> tokens)
        {
            return (tokens.Where(t => t.GetTokenType().Equals(TokenType.Comma)).Count() > 0);
        }
    }
}
