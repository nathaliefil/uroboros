using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.reading
{
    class TokenGroups
    {
        private static TokenType[] ALLOWED_SUBCOMMAND = new TokenType[] { TokenType.Where, TokenType.First, 
            TokenType.Last, TokenType.Skip,  TokenType.Each, TokenType.OrderBy};

        private static TokenType[] ALLOWED_EXPRESSION = new TokenType[] { TokenType.Plus, TokenType.Minus, 
            TokenType.Multiply, TokenType.Divide,  TokenType.Percent, TokenType.And, 
            TokenType.Or, TokenType.Xor,  TokenType.BracketOn, TokenType.BracketOff, 
            TokenType.SquareBracketOn, TokenType.SquareBracketOff,  TokenType.Comma, TokenType.Equals, 
            TokenType.Bigger, TokenType.Smaller,  TokenType.BiggerOrEquals, TokenType.SmallerOrEquals, 
            TokenType.Exclamation, TokenType.NotEquals,  TokenType.Variable, TokenType.StringConstant, 
            TokenType.NumericConstant, TokenType.BoolConstant};

        private static TokenType[] COMPARING = new TokenType[] { TokenType.Equals, TokenType.NotEquals, 
            TokenType.Smaller, TokenType.SmallerOrEquals,  TokenType.Bigger, TokenType.BiggerOrEquals};

        private static TokenType[] LOGIC= new TokenType[] { TokenType.Exclamation, TokenType.Or, 
            TokenType.Xor, TokenType.And};



        public static bool IsSubcommandKeyword(TokenType type)
        {
            return ALLOWED_SUBCOMMAND.Contains(type) ? true : false;
        }

        public static Token WrongTokenInExpression(List<Token> tokens)
        {
            foreach (Token tok in tokens)
            {
                if (!ALLOWED_EXPRESSION.Contains(tok.GetTokenType()))
                    return tok;
            }
            return new Token(TokenType.Null);
        }

        public static bool IsComparingSign(TokenType type)
        {
            return COMPARING.Contains(type) ? true : false;
        }

        public static bool IsLogicSign(TokenType type)
        {
            return LOGIC.Contains(type) ? true : false;
        }
    }
}
