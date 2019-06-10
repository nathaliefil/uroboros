using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.expressions
{
    class BoolExpression : Variable, IBoolable, INumerable, IStringable
    {
        List<IBoolable> elements;

        BoolExpression(List<IBoolable> elements)
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
                return "true";
            else
                return "false";
        }
    }
}
