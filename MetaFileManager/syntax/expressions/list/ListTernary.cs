using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools
{
    class ListTernary : DefaultListable
    {
        private IBoolable condition;
        private IListable leftValue;
        private IListable rightValue;

        public ListTernary(IBoolable condition, IListable leftValue, IListable rightValue)
        {
            this.condition = condition;
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        public override List<string> ToList()
        {
            return condition.ToBool() ? leftValue.ToList() : rightValue.ToList();
        }
    }
}
