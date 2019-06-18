using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.expressions
{
    class NumericExpression : INumerable
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

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
