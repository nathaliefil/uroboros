using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncPower : INumericFunction
    {
        private INumerable arg0;
        private INumerable arg1;

        public FuncPower(INumerable arg0, INumerable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public decimal ToNumber()
        {
            return (decimal)Math.Pow((double)arg0.ToNumber(), (double)arg1.ToNumber());
            // maybe there is a better way to do that (without losing decimal precision)
            // don't know
            /// todo
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            if (value % 1 == 0)
                return ((int)value).ToString();
            else
                return value.ToString();
        }
    }
}
