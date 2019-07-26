using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.expressions.numeric;

namespace Uroboros.syntax.interpretation.expressions
{
    class NegatedNumerableBuilder
    {
        public static INumerable Build(INumerable inum)
        {
            if (inum is NumericConstant)
            {
                (inum as NumericConstant).SetNegative();
                return inum;
            }
            else
                return new NegatedNumerable(inum);
        }
    }
}
