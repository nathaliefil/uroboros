using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.expressions
{
    class BoolExpression : DefaultToListMethod, IBoolable
    {
        List<IBoolable> elements;

        public BoolExpression(List<IBoolable> elements)
        {
            this.elements = elements;
        }

        public bool ToBool()
        {
            // here will be a lot of code
            /// todo
            return true;
        }

        public decimal ToNumber()
        {
            bool value = ToBool();
            if (value)
                return 1;
            else
                return 0;
        }

        public override string ToString()
        {
            bool value = ToBool();
            if (value)
                return "1";
            else
                return "0";
        }
    }
}
