using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncNumber : DefaultNumerable
    {
        private IStringable arg0;

        public FuncNumber(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            try
            {
                decimal value = Decimal.Parse(arg0.ToString());
                return value;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
