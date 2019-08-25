using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.strings
{
    class FuncSubstring__2args : DefaultStringable
    {
        private IStringable arg0;
        private INumerable arg1;

        public FuncSubstring__2args(IStringable arg0, INumerable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override string ToString()
        {
            string source = arg0.ToString();
            int arg1v = (int)arg1.ToNumber();

            if (arg1v >= source.Length)
                return base.ToString();

            return source.Substring(arg1v);
        }
    }
}
