using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncCount : DefaultNumerable
    {
        private IListable arg0;

        public FuncCount(IListable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return arg0.ToList().Count;
        }
    }
}
