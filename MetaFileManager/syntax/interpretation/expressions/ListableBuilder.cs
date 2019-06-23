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

namespace Uroboros.syntax.interpretation.expressions
{
    class ListableBuilder
    {
        public static IListable Build(List<Token> tokens)
        {
            IStringable ist = StringableBuilder.Build(tokens);
            if (!(ist is NullVariable))
                return ist;

            if (tokens.Count == 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.Variable)
                && tokens[0].GetContent().ToLower().Equals("empty") && tokens[1].GetContent().ToLower().Equals("list"))
                return new EmptyList();

            IListable listed = ListableBuilder.BuildListedByComma(tokens);
            if (!(listed is NullVariable))
                return listed;

            string str = tokens[0].GetContent();
            if (!InterVariables.GetInstance().Contains(str, InterVarType.List))
                throw new SyntaxErrorException("ERROR! Variable " + str +" do not exist or cannot be read as list.");

            if (tokens.Count == 1 && tokens[0].GetTokenType().Equals(TokenType.Variable))
                return new ListVariableRefer(str);

            if (!TokenGroups.IsSubcommandKeyword(tokens[1].GetTokenType()))
                throw new SyntaxErrorException("ERROR! In list declaration description do not start from keyword.");
            
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
            {
                list.AddSubcommand(SubcommandBuilder.Build(currentTokens, subcommandType));
            }

            return list;
        }
        
        public static IListable BuildListedByComma(List<Token> tokens)
        {
            List<Token> currentTokens = new List<Token>();
            List<IStringable> elements = new List<IStringable>();
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
                        IStringable ist = StringableBuilder.Build(currentTokens);
                        currentTokens.Clear();
                        if (ist is NullVariable)
                            return new NullVariable();
                        else
                            elements.Add(ist);
                    }
                }
                else
                    currentTokens.Add(tokens[i]);
            }

            if (currentTokens.Count > 0)
            {
                IStringable ist = StringableBuilder.Build(currentTokens);
                if (ist is NullVariable)
                    return new NullVariable();
                else
                    elements.Add(ist);
            }

            if (elements.Count == 0)
                return new NullVariable();

            if (elements.All(e => e is StringConstant))
                return new ListConstant(elements.Select(e => e.ToString()).ToList());
            else
                return new ListedStringables(elements);
        }
    }
}
