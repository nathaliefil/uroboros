﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncCeil : DefaultNumerable
    {
        private INumerable arg0;

        public FuncCeil(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return Decimal.Floor(arg0.ToNumber()) + 1;
            // function Decimal.Ceiling do not work - don't know why
        }
    }
}
