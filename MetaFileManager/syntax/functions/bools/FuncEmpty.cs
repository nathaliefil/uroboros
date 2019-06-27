using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.bools
{
    class FuncEmpty : DefaultBoolable
    {
        private IListable arg0;

        public FuncEmpty(IListable arg0)
        {
            this.arg0 = arg0;
        }

        public override bool ToBool()
        {
            return arg0.ToList().Count() == 0 ? true : false;
        }
    }
}
