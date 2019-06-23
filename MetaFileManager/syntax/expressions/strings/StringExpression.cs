using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.strings
{
    class StringExpression : DefaultToListMethod, IStringable
    {
        List<IStringable> elements;

        public StringExpression(List<IStringable> elements)
        {
            this.elements = elements;
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

        public void Add(IStringable element)
        {
            elements.Add(element);
        }
    }
}
