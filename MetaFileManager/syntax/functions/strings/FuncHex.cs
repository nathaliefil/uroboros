using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.strings
{
    class FuncHex : DefaultStringable
    {
        private INumerable arg0;

        public FuncHex(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            int number = (int)arg0.ToNumber();
            if (number > 0)
                return Convert.ToString(number, 16);
            else
                number *= -1;
                return "-" + Convert.ToString(number, 16);
        }
    }
}
