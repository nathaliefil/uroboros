using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncRound : INumericFunction
    {
        private INumerable arg0;

        public FuncRound(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public decimal ToNumber()
        {
            return Decimal.Round(arg0.ToNumber());
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            return ((int)value).ToString();
        }
    }
}
