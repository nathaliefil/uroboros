﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.strings
{
    class FuncWeekday : DefaultStringable
    {
        private INumerable arg0;

        public FuncWeekday(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return DateExtractor.WeekDay(arg0.ToNumber());
        }
    }
}
