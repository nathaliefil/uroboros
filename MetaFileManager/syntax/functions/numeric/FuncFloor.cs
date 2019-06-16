using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FloorCeil : INumericFunction
    {
        private INumerable arg0;

        public FloorCeil(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public decimal ToNumber()
        {
            return Decimal.Floor(arg0.ToNumber());
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            return ((int)value).ToString();
        }

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
