using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;
using DivineScript.syntax.reading;
using DivineScript.syntax.interpretation.vars_range;
using DivineScript.syntax.variables.refers;
using DivineScript.syntax.variables.expressions.list;

namespace DivineScript.syntax.interpretation.expressions
{
    class ListableBuilder
    {
        public static IListable Build(List<Token> tokens)
        {
            IStringable ist = StringableBuilder.Build(tokens);
            if (!(ist is NullVariable))
                return ist;

            string str = tokens[0].GetContent();
            if (!InterVariables.GetInstance().Contains(str, InterVarType.List))
                throw new SyntaxErrorException("ERROR! In list declaration variable " + str +" do not exist or cannot be read as list.");

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
    }
}
