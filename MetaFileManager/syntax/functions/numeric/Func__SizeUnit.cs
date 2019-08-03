using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class Func__SizeUnit : DefaultNumerable
    {
        private INumerable arg0;
        private SizeSufix sizeSufix;

        public Func__SizeUnit(INumerable arg0, SizeSufix sizeSufix)
        {
            this.arg0 = arg0;
            this.sizeSufix = sizeSufix;
        }

        public override decimal ToNumber()
        {
            return arg0.ToNumber() * SizeUnit.GetMultiplier(sizeSufix);
        }
    }
}
