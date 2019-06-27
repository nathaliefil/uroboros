using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncE : DefaultNumerable
    {
        public FuncE()
        {
        }

        public override decimal ToNumber()
        {
            return 2.71828182845904M;
        }
    }
}
