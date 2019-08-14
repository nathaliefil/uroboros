using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools
{
    class BoolTernary : DefaultBoolable
    {
        private IBoolable condition;
        private IBoolable leftValue;
        private IBoolable rightValue;

        public BoolTernary(IBoolable condition, IBoolable leftValue, IBoolable rightValue)
        {
            this.condition = condition;
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        public override bool ToBool()
        {
            return condition.ToBool() ? leftValue.ToBool() : rightValue.ToBool();
        }
    }
}
