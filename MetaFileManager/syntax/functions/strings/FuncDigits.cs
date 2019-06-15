using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.functions.numeric
{
    class FuncDigits : IStringFunction
    {
        private StringExpression arg0;

        public FuncDigits(StringExpression arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            string value = arg0.ToString();
            if (string.IsNullOrEmpty(value)) return value;
            return new string(value.Where(char.IsDigit).ToArray());
        }
    }
}
