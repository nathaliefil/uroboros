using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.functions.numeric
{
    class FuncBinary : IStringFunction
    {
        private NumericExpression arg0;

        public FuncBinary(NumericExpression arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return Convert.ToString((int)arg0.ToNumber(), 2);
        }
    }
}
