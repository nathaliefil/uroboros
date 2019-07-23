using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.numeric
{
    class NegatedNumerable : DefaultNumerable
    {
        private INumerable content;

        public NegatedNumerable(INumerable content)
        {
            this.content = content;
        }

        public override decimal ToNumber()
        {
            return -content.ToNumber();
        }
    }
}
