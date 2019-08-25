using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.strings
{
    class FuncSubstring__3args : DefaultStringable
    {
        private IStringable arg0;
        private INumerable arg1;
        private INumerable arg2;

        public FuncSubstring__3args(IStringable arg0, INumerable arg1, INumerable arg2)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public override string ToString()
        {
            string source = arg0.ToString();

            int arg1v = (int)arg1.ToNumber();
            if (arg1v >= source.Length)
                return "";

            int arg2v = (int)arg2.ToNumber();
            if (arg2v < 1)
                return "";

            if (arg1v + arg2v > source.Length)
                arg2v -= arg1v + arg2v - source.Length;

            return source.Substring(arg1v, arg2v);
        }
    }
}
