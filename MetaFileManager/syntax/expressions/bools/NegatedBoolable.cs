using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools
{
    class NegatedBoolable : DefaultToListMethod, IBoolable
    {
        private IBoolable content;

        public NegatedBoolable(IBoolable content)
        {
            this.content = content;
        }

        public bool ToBool()
        {
            return !content.ToBool();
        }

        public override string ToString()
        {
            if (!content.ToBool())
                return "1";
            else
                return "0";
        }

        public decimal ToNumber()
        {
            if (!content.ToBool())
                return 1;
            else
                return 0;
        }
    }
}
