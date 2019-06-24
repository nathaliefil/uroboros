using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.expressions.bools.other
{
    class In : DefaultToListMethod, IBoolable
    {
        private IStringable value;
        private IListable compared;

        public In(IStringable value, IListable compared)
        {
            this.value = value;
            this.compared = compared;
        }

        public bool ToBool()
        {
            return compared.ToList().Any(e => e.ToString().Equals(value.ToString()));
        }

        public decimal ToNumber()
        {
            return ToBool() ? 1 : 0;
        }

        public override string ToString()
        {
            return ToBool() ? "1" : "0";
        }
    }
}
