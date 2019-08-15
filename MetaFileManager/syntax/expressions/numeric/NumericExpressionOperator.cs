using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.expressions.numeric
{
    class NumericExpressionOperator : INumericExpressionElement
    {
        private NumericExpressionOperatorType type;

        public NumericExpressionOperator(NumericExpressionOperatorType type)
        {
            this.type = type;
        }

        public NumericExpressionOperatorType GetOperatorType()
        {
            return type;
        }

        public void ReverseBracket()
        {
            if (type.Equals(NumericExpressionOperatorType.BracketOn))
                type = NumericExpressionOperatorType.BracketOff;
            else if (type.Equals(NumericExpressionOperatorType.BracketOff))
                type = NumericExpressionOperatorType.BracketOn;
        }

        public void UnaryMinus()
        {
            type = NumericExpressionOperatorType.SignChange;
        }

        public bool IsBinaryOperator()
        {
            return type.Equals(NumericExpressionOperatorType.Plus) ||
                   type.Equals(NumericExpressionOperatorType.Minus) ||
                   type.Equals(NumericExpressionOperatorType.Multiply) ||
                   type.Equals(NumericExpressionOperatorType.Divide) ||
                   type.Equals(NumericExpressionOperatorType.Modulo);
        }

        public bool IsSignChange()
        {
            return type.Equals(NumericExpressionOperatorType.SignChange);
        }

        public bool IsBracketOn()
        {
            return type.Equals(NumericExpressionOperatorType.BracketOn);
        }

        public bool IsBracketOff()
        {
            return type.Equals(NumericExpressionOperatorType.BracketOff);
        }

        public bool IsMinus()
        {
            return type.Equals(NumericExpressionOperatorType.Minus);
        }
    }
}
