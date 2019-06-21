using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.reading
{
    class Brackets
    {
        public static void CheckCorrectness(List<Token> tokens, bool withCurlyBrackets)
        {
            if (!Brackets.AreCorrect(tokens, BracketsType.Normal))
                throw new SyntaxErrorException("ERROR! Check brackets ( ).");
            if (!Brackets.AreCorrect(tokens, BracketsType.Square))
                throw new SyntaxErrorException("ERROR! Check square brackets [ ].");
            if (withCurlyBrackets && !Brackets.AreCorrect(tokens, BracketsType.Curly))
                throw new SyntaxErrorException("ERROR! Check curly brackets { }.");
        }

        private static bool AreCorrect(List<Token> tokens, BracketsType type)
        {
            TokenType open = TokenType.BracketOn;
            TokenType close = TokenType.BracketOff;
            int level = 0;

            switch (type)
            {
                case BracketsType.Curly:
                    open = TokenType.CurlyBracketOn;
                    close = TokenType.CurlyBracketOff;
                    break;
                case BracketsType.Square:
                    open = TokenType.SquareBracketOn;
                    close = TokenType.SquareBracketOff;
                    break;
            }

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

    public enum BracketsType
    {
        Normal,
        Curly,
        Square
    }
}
