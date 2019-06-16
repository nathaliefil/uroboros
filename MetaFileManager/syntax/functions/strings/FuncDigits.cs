using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncDigits : IStringFunction
    {
        private IStringable arg0;

        public FuncDigits(IStringable arg0)
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
