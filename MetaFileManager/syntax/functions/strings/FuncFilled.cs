using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncFilled : DefaultStringable
    {
        private IStringable arg0;
        private INumerable arg1;

        public FuncFilled(IStringable arg0, INumerable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override string ToString()
        {
            string value;
            int number = (int)arg1.ToNumber();

            if (arg0 is INumerable)
            {
                if ((arg0 as INumerable).ToNumber() % 1 == 0)
                    value = ((int)(arg0 as INumerable).ToNumber()).ToString();
                else
                    value = (arg0 as INumerable).ToNumber().ToString();
            }
            else
                value = arg0.ToString();

            if (value.Length >= number)
                return value;

            return string.Concat(Enumerable.Repeat("0", number - value.Length)) + value;
        }
    }
}
