using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncLog10 : DefaultNumerable
    {
        private INumerable arg0;

        public FuncLog10(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            decimal number = arg0.ToNumber();

            if (number == 0)
                throw new RuntimeException("RUNTIME ERROR! Binary logarithm of zero happened.");
            else if (number < 0)
                throw new RuntimeException("RUNTIME ERROR! Binary logarithm of negative number happened.");
            else return (decimal)Math.Log10(Decimal.ToDouble(number));
        }
    }
}
