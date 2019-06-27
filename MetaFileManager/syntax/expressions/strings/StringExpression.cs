using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.strings
{
    class StringExpression : DefaultStringable
    {
        List<IStringable> elements;

        public StringExpression(List<IStringable> elements)
        {
            this.elements = elements;
        }

        public void Add(IStringable element)
        {
            elements.Add(element);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IStringable element in elements)
            {
                sb.Append(element.ToString());
            }
            return sb.ToString();
        }
    }
}
