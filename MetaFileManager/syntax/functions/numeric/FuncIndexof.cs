using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncIndexof : DefaultNumerable
    {
        private IStringable arg0;
        private IStringable arg1;

        public FuncIndexof(IStringable arg0, IStringable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override decimal ToNumber()
        {
            return arg0.ToString().IndexOf(arg1.ToString());
        }
    }
}
