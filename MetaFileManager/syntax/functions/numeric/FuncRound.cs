using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.functions.numeric.abstracts;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncRound : INumericFunction
    {
        private INumerable arg0;

        public FuncRound(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return Decimal.Round(arg0.ToNumber());
        }
    }
}
