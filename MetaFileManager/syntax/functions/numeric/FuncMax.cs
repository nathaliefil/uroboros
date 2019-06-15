using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.functions.numeric
{
    class FuncMax : INumericFunction
    {
        private List<NumericExpression> arg0;

        public FuncMax(List<NumericExpression> arg0)
        {
            this.arg0 = arg0;
        }

        public decimal ToNumber()
        {
            return arg0.Max(x => x.ToNumber());
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
