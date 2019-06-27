using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools
{
    class NegatedBoolable : DefaultBoolable
    {
        private IBoolable content;

        public NegatedBoolable(IBoolable content)
        {
            this.content = content;
        }

        public override bool ToBool()
        {
            return !content.ToBool();
        }
    }
}
