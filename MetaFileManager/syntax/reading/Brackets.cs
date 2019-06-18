using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.reading
{
    class Brackets
    {
        public static bool AreCorrect(List<Token> tokens, bool curly)
        {
            TokenType open = curly ? TokenType.CurlyBracketOn : TokenType.BracketOn;
            TokenType close = curly ? TokenType.CurlyBracketOff : TokenType.BracketOff;
            int level = 0;

            foreach (Token token in tokens) 
            {
                if (token.GetTokenType().Equals(open))
                    level++;
                if (token.GetTokenType().Equals(close))
                    level--;
                if (level < 0)
                    return false;
            }
            return level == 0 ? true : false;
        }
    }
}
