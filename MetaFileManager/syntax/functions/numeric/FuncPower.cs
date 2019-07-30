using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncPower : DefaultNumerable
    {
        private INumerable arg0;
        private INumerable arg1;

        public FuncPower(INumerable arg0, INumerable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override decimal ToNumber()
        {
            return (decimal)Math.Pow((double)arg0.ToNumber(), (double)arg1.ToNumber());
        }
    }
}
