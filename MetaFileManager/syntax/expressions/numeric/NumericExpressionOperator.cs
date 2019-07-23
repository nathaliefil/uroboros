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
    }
}
