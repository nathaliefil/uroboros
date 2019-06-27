using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncMin : DefaultNumerable
    {
        private List<INumerable> arg0;

        public FuncMin(List<INumerable> arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return arg0.Min(x => x.ToNumber()); 
        }
    }
}
