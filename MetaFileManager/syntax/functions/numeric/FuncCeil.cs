using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncCeil : INumericFunction
    {
        private INumerable arg0;

        public FuncCeil(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return Decimal.Ceiling(arg0.ToNumber());
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            return ((int)value).ToString();
        }
    }
}
