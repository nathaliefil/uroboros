using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncYear : DefaultNumerable
    {
        private IStringable arg0;

        public FuncYear(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            StringBuilder stringb = new StringBuilder();

            foreach (char ch in arg0.ToString())
            {
                if (Char.IsDigit(ch))
                    stringb.Append(ch);
                else
                {
                    if (stringb.Length == 4)
                        return Decimal.Parse(stringb.ToString());
                    else
                    {
                        if (stringb.Length>0)
                            stringb.Clear();
                    }
                }
            }

            if (stringb.Length == 4)
                return Decimal.Parse(stringb.ToString());
            else
                return 0;
        }
    }
}