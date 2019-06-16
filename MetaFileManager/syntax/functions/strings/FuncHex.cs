using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncHex : IStringFunction
    {
        private INumerable arg0;

        public FuncHex(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return Convert.ToString((int)arg0.ToNumber(), 16);
        }

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
