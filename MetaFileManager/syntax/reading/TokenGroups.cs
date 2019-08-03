using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.bools;
using Uroboros.syntax.expressions.numeric;

namespace Uroboros.syntax.reading
{
    class TokenGroups
    {
        private static TokenType[] ALLOWED_SUBCOMMAND = new TokenType[] { TokenType.Where, TokenType.First, 
            TokenType.Last, TokenType.Skip,  TokenType.Each, TokenType.OrderBy, TokenType.With, TokenType.Without};

        private static TokenType[] ALLOWED_IN_EXPRESSION = new TokenType[] { TokenType.Plus, TokenType.Minus, 
            TokenType.Multiply, TokenType.Divide,  TokenType.Percent, TokenType.And, 
            TokenType.Or, TokenType.Xor,  TokenType.BracketOn, TokenType.BracketOff, 
            TokenType.SquareBracketOn, TokenType.SquareBracketOff,  TokenType.Comma, TokenType.Equals, 
            TokenType.Bigger, TokenType.Smaller,  TokenType.BiggerOrEquals, TokenType.SmallerOrEquals, 
            TokenType.Exclamation, TokenType.NotEquals,  TokenType.Variable, TokenType.StringConstant, 
            TokenType.NumericConstant, TokenType.BoolConstant, TokenType.In, TokenType.Like, TokenType.Is,
            TokenType.After, TokenType.Before, TokenType.IsAfter, TokenType.IsBefore};

        private static TokenType[] COMPARING = new TokenType[] { TokenType.Equals, TokenType.NotEquals, 
            TokenType.Smaller, TokenType.SmallerOrEquals,  TokenType.Bigger, TokenType.BiggerOrEquals};

        private static TokenType[] TIME_COMPARING = new TokenType[] { TokenType.IsAfter, TokenType.IsBefore };

        private static TokenType[] LOGIC= new TokenType[] { TokenType.Exclamation, TokenType.Or, 
            TokenType.Xor, TokenType.And};

        private static TokenType[] NUMERIC_OPERATION = new TokenType[] { TokenType.Plus, TokenType.Minus, 
            TokenType.Multiply, TokenType.Divide, TokenType.Percent};

        private static TokenType[] CORE_COMMAND = new TokenType[] { TokenType.Copy, TokenType.Cut, 
            TokenType.Delete, TokenType.Drop,  TokenType.Open, TokenType.Move, TokenType.Rename
            , TokenType.CreateFile, TokenType.CreateDirectory};

        private static TokenType[] VARIABLE_OPERATION = new TokenType[] { TokenType.PlusEquals, TokenType.MinusEquals, 
            TokenType.MultiplyEquals, TokenType.DivideEquals, TokenType.PercentEquals };

        
        

        public static Token WrongTokenInExpression(List<Token> tokens)
        {
            foreach (Token tok in tokens)
            {
                if (!ALLOWED_IN_EXPRESSION.Contains(tok.GetTokenType()))
                    return tok;
            }
            return new Token(TokenType.Null);
        }

        public static bool IsSubcommandKeyword(TokenType type)
        {
            return ALLOWED_SUBCOMMAND.Contains(type) ? true : false;
        }

        public static bool IsComparingSign(TokenType type)
        {
            return COMPARING.Contains(type) ? true : false;
        }

        public static bool IsTimeComparingSign(TokenType type)
        {
            return TIME_COMPARING.Contains(type) ? true : false;
        }

        public static bool IsLogicSign(TokenType type)
        {
            return LOGIC.Contains(type) ? true : false;
        }

        public static bool IsArithmeticSign(TokenType type)
        {
            return NUMERIC_OPERATION.Contains(type) ? true : false;
        }

        public static bool IsCoreCommandKeyword(TokenType type)
        {
            return CORE_COMMAND.Contains(type) ? true : false;
        }

        public static bool IsVariableOperation(TokenType type)
        {
            return VARIABLE_OPERATION.Contains(type) ? true : false;
        }
    }
}
