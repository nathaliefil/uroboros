using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools
{
    class NumericTernary : DefaultNumerable
    {
        private IBoolable condition;
        private INumerable leftValue;
        private INumerable rightValue;

        public NumericTernary(IBoolable condition, INumerable leftValue, INumerable rightValue)
        {
            this.condition = condition;
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        public override decimal ToNumber()
        {
            return condition.ToBool() ? leftValue.ToNumber() : rightValue.ToNumber();
        }
    }
}
