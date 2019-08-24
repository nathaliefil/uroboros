using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.lexer;

namespace Uroboros.syntax.interpretation.functions
{
    class Functions
    {
        public static bool IsPossibleFunction(List<Token> tokens)
        {
            if (tokens.Count < 3)
                return false;

            if (!tokens[0].GetTokenType().Equals(TokenType.Variable))
                return false;

            if (!tokens[1].GetTokenType().Equals(TokenType.BracketOn))
                return false;

            int level = 0;
            int index = 0;

            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.BracketOn))
                    level++;
                else if (tok.GetTokenType().Equals(TokenType.BracketOff))
                {
                    level--;
                    if (level == 0)
                        break;
                }
                index++;
            }

            return index == tokens.Count - 1;
        }
    }
}
