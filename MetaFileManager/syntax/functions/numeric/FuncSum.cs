using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncSum : DefaultNumerable
    {
        private List<INumerable> arg0;

        public FuncSum(List<INumerable> arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return arg0.Sum(x => x.ToNumber());
        }
    }
}
