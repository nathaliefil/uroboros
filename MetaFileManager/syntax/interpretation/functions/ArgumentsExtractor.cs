using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;

namespace Uroboros.syntax.interpretation.functions
{
    class ArgumentsExtractor
    {
        public static List<Argument> GetArguments(List<Token> tokens)
        {
            List<Argument> arguments = new List<Argument>();

            tokens.RemoveAt(tokens.Count - 1);
            tokens.RemoveAt(0);

            if (tokens.Count == 0)
                return arguments;

            List<Token> currentTokens = new List<Token>();
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
                        arguments.Add(new Argument(currentTokens));
                        currentTokens.Clear();
                    }
                }
                else
                    currentTokens.Add(tokens[i]);
            }

            if (currentTokens.Count > 0)
            {
                arguments.Add(new Argument(currentTokens));
                currentTokens.Clear();
            }

            return arguments;
        }
    }
}
