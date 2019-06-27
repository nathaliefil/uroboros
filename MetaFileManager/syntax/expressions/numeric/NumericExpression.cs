﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.numeric
{
    class NumericExpression : DefaultNumerable
    {
        List<INumerable> elements;

        public NumericExpression(List<INumerable> elements)
        {
            this.elements = elements;
        }

        public override decimal ToNumber()
        {
            //here a lot of code
            /// todo
            return 0;
        }
    }
}
