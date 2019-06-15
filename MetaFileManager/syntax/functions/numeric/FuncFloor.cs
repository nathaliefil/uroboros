using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.functions.numeric
{
    class FloorCeil : INumericFunction
    {
        private NumericExpression arg0;

        public FloorCeil(NumericExpression arg0)
        {
            this.arg0 = arg0;
        }

        public decimal ToNumber()
        {
            return Decimal.Floor(arg0.ToNumber());
        }

        public string ToString()
        {
            decimal value = ToNumber();

            return ((int)value).ToString();
        }
    }
}
