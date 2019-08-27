using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools.other
{
    class BetweenTimes : DefaultBoolable
    {
        private ITimeable value;
        private ITimeable leftBound;
        private ITimeable rightBound;

        public BetweenTimes(ITimeable value, ITimeable leftBound, ITimeable rightBound)
        {
            this.value = value;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }

        public override bool ToBool()
        {
            DateTime left = leftBound.ToTime();
            DateTime right = rightBound.ToTime();
            DateTime v = value.ToTime();

            if (left < right)
                return (v > left) && (v < right);
            else if (left > right)
                return (v < left) && (v > right);
            else
                return false;
        }
    }
}
