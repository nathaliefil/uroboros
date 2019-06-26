using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.functions.numeric.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncE : INumericFunction
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
