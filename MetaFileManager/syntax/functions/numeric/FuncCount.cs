using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.variables.expressions.list;

namespace DivineScript.syntax.functions.numeric
{
    class FuncCount : INumericFunction
    {
        private ListExpression arg0;

        public FuncCount(ListExpression arg0)
        {
            this.arg0 = arg0;
        }

        public decimal ToNumber()
        {
            return arg0.ToList().Count;
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            return ((int)value).ToString();
        }
    }
}
