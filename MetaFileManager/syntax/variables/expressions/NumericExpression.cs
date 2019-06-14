using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.expressions
{
    class NumericExpression : Variable, INumerable, IStringable
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
            decimal value = ToNumber();
            if (value % 1 == 0)
                return ((int)value).ToString();
            else
                return value.ToString();
        }
    }
}
