using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncLn : DefaultNumerable
    {
        private INumerable arg0;

        public FuncLn(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            decimal number = arg0.ToNumber();

            if (number == 0)
                throw new RuntimeException("RUNTIME ERROR! Natural logarithm of zero happened.");
            else if (number < 0)
                throw new RuntimeException("RUNTIME ERROR! Natural logarithm of negative number happened.");
            else return (decimal)Math.Log(Decimal.ToDouble(number));
        }
    }
}
