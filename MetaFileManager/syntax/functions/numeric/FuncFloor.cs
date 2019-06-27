using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FloorCeil : DefaultNumerable
    {
        private INumerable arg0;

        public FloorCeil(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return Decimal.Floor(arg0.ToNumber());
        }
    }
}
