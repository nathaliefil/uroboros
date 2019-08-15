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
    }
}
