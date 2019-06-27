using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.bools
{
    class FuncContain : DefaultBoolable
    {
        private IListable arg0;
        private IStringable arg1;

        public FuncContain(IListable arg0, IStringable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override bool ToBool()
        {
            return arg0.ToList().Contains(arg1.ToString()) ? true : false;
        }
    }
}
