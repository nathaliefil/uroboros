﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.functions.strings.abstracts;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncTrim : IStringFunction
    {
        private IStringable arg0;

        public FuncTrim(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return arg0.ToString().Trim();
        }
    }
}