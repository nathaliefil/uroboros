﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools.comparisons
{
    abstract class Comparison : DefaultToListMethod, IBoolable
    {
        public virtual bool ToBool()
        {
            return false; // to override
        }

        public decimal ToNumber()
        {
            return ToBool() ? 1 : 0;
        }

        public override string ToString()
        {
            return ToBool() ? "1" : "0";
        }
    }
}