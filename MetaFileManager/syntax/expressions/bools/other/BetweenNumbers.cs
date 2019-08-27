using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools.other
{
    class BetweenNumbers : DefaultBoolable
    {
        private INumerable value;
        private INumerable leftBound;
        private INumerable rightBound;

        public BetweenNumbers(INumerable value, INumerable leftBound, INumerable rightBound)
        {
            this.value = value;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }

        public override bool ToBool()
        {
            decimal left = leftBound.ToNumber();
            decimal right = rightBound.ToNumber();
            decimal v = value.ToNumber();

            if (left < right)
                return (v > left) && (v < right);
            else if (left > right)
                return (v < left) && (v > right);
            else 
                return false;
        }
    }
}
