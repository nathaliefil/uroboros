using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.expressions
{
    class NumericExpression : DefaultToListMethod, INumerable
    {
        List<INumerable> elements;

        public NumericExpression(List<INumerable> elements)
        {
            this.elements = elements;
        }

        public decimal ToNumber()
        {
            //here a lot of code
            /// todo
            return 0;
        }

        public override string ToString()
        {
            return ToNumber().ToString();
        }
    }
}
