using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class NumericConstant : DefaultNumerable
    {
        private decimal value;

        public NumericConstant(decimal value)
        {
            this.value = value;
        }

        public override decimal ToNumber()
        {
            return value;
        }

        public void SetNegative()
        {
            value *= -1;
        }
    }
}
