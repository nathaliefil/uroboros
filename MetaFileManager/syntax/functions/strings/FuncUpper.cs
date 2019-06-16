using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncUpper : IStringFunction
    {
        private IStringable arg0;

        public FuncUpper(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return (arg0.ToString()).ToUpper();
        }

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
