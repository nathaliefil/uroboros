using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncAverage : DefaultNumerable
    {
        private List<INumerable> arg0;

        public FuncAverage(List<INumerable> arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return arg0.Average(x => x.ToNumber());
        }
    }
}
