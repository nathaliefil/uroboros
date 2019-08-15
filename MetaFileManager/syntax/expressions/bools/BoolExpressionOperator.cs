using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.expressions.bools
{
    class BoolExpressionOperator : IBoolExpressionElement
    {
        private BoolExpressionOperatorType type;

        public BoolExpressionOperator(BoolExpressionOperatorType type)
        {
            this.type = type;
        }

        public BoolExpressionOperatorType GetOperatorType()
        {
            return type;
        }

        public void ReverseBracket()
        {
            if (type.Equals(BoolExpressionOperatorType.BracketOn))
                type = BoolExpressionOperatorType.BracketOff;
            else if (type.Equals(BoolExpressionOperatorType.BracketOff))
                type = BoolExpressionOperatorType.BracketOn;
        }

        public bool IsBinaryOperator()
        {
            return type.Equals(BoolExpressionOperatorType.And) ||
                   type.Equals(BoolExpressionOperatorType.Or) ||
                   type.Equals(BoolExpressionOperatorType.Xor);
        }

        public bool IsNegation()
        {
            return type.Equals(BoolExpressionOperatorType.Not);
        }

        public bool IsBracketOn()
        {
            return type.Equals(BoolExpressionOperatorType.BracketOn);
        }

        public bool IsBracketOff()
        {
            return type.Equals(BoolExpressionOperatorType.BracketOff);
        }
    }
}
