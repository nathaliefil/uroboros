﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.functions.strings.abstracts;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncHex : IStringFunction
    {
        private INumerable arg0;

        public FuncHex(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return Convert.ToString((int)arg0.ToNumber(), 16);
        }
    }
}
