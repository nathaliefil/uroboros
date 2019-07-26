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
            // try to build Strinable
            IStringable ist = StringableBuilder.Build(tokens);
            if (!ist.IsNull())
                return (ist as IListable);

            // try to build 'empty list'
            if (tokens.Count == 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.Variable)
                && tokens[0].GetContent().ToLower().Equals("empty") && tokens[1].GetContent().ToLower().Equals("list"))
                return new EmptyList();

            // try to build listed strings: many Stringables divided by commas
            IListable listed = ListedStringablesBuilder.Build(tokens);
            if (!listed.IsNull())
                return listed;

            // take first token and check if is a name of existing list
            string str = tokens[0].GetContent();
            if (!InterVariables.GetInstance().Contains(str, InterVarType.List))
                throw new SyntaxErrorException("ERROR! Variable " + str +" do not exist or cannot be read as list.");

            // try to build list variable reference - just one word and it is a name
            if (tokens.Count == 1 && tokens[0].GetTokenType().Equals(TokenType.Variable))
                return new ListVariableRefer(str);

            // take second word and check if it is subcommand keyword (first, last, where...)
            if (!TokenGroups.IsSubcommandKeyword(tokens[1].GetTokenType()))
                throw new SyntaxErrorException("ERROR! In list declaration description do not start from keyword.");
            
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
    }
}
