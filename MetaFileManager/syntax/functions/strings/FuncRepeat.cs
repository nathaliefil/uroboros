using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncRepeat : DefaultStringable
    {
        private IStringable arg0;
        private INumerable arg1;

        public FuncRepeat(IStringable arg0, INumerable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override string ToString()
        {
            int repeats = (int)arg1.ToNumber();
            if (repeats <= 0)
                return "";
            else
                return string.Concat(Enumerable.Repeat(arg0.ToString(), repeats));
        }
    }
}
