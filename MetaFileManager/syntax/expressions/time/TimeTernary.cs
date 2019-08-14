using Uroboros.syntax.variables.abstracts;
using System;

namespace Uroboros.syntax.expressions.bools
{
    class TimeTernary : DefaultTimeable
    {
        private IBoolable condition;
        private ITimeable leftValue;
        private ITimeable rightValue;

        public TimeTernary(IBoolable condition, ITimeable leftValue, ITimeable rightValue)
        {
            this.condition = condition;
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        public override DateTime ToTime()
        {
            return condition.ToBool() ? leftValue.ToTime() : rightValue.ToTime();
        }
    }
}
