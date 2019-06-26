using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.functions.numeric.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncPi : INumericFunction
    {
        public FuncPi()
        {
        }

        public override decimal ToNumber()
        {
            return 3.14159265358979M;
        }
    }
}
