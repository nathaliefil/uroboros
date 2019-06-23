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
    }
}
