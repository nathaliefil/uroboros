﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.strings
{
    class FuncMonth : DefaultStringable
    {
        private INumerable arg0;

        public FuncMonth(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return DateExtractor.Month(arg0.ToNumber());
        }
    }
}
