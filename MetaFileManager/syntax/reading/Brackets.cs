using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.reading
{
    class Brackets
    {
        public static void InformAboutCorrectness(List<Token> tokens)
        {
            if (tokens.Count == 0)
                return;
            if (!Brackets.AreCorrect(tokens, BracketsType.Normal))
                throw new SyntaxErrorException("ERROR! Check brackets ( ).");
            if (!Brackets.AreCorrect(tokens, BracketsType.Square))
                throw new SyntaxErrorException("ERROR! Check square brackets [ ].");
            if (!Brackets.AreCorrect(tokens, BracketsType.Curly))
                throw new SyntaxErrorException("ERROR! Check curly brackets { }.");
        }

        public static bool CheckCorrectness(List<Token> tokens)
        {
            if (tokens.Count == 0)
                return false;
            if (!Brackets.AreCorrect(tokens, BracketsType.Normal))
                return false;
            if (!Brackets.AreCorrect(tokens, BracketsType.Square))
                return false;
            return true;
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

        public static bool ContainsIndependentBracketsPairs(List<Token> tokens, BracketsType type)
        {
            TokenType open = TokenType.BracketOn;
            TokenType close = TokenType.BracketOff;

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

            int level = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].GetTokenType().Equals(open))
                    level++;
                if (tokens[i].GetTokenType().Equals(close))
                    level--;
                if (level == 0 && i != tokens.Count - 1 && i != 0)
                    return true;
                
            }
            return false;
        }
    }

    public enum BracketsType
    {
        Normal,
        Curly,
        Square
    }
}
