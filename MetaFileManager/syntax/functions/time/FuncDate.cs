﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.time
{
    class FuncDate : DefaultTimeable
    {
        private INumerable arg0;
        private INumerable arg1;
        private INumerable arg2;

        public FuncDate(INumerable arg0, INumerable arg1, INumerable arg2)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public override DateTime ToTime()
        {
            return new DateTime((int)arg2.ToNumber(), (int)arg1.ToNumber(), (int)arg0.ToNumber(), 0, 0, 0);
        }
    }
}
