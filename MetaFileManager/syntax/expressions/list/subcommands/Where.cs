using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.list.subcommands
{
    class Where : ISubcommand
    {
        private IBoolable condition;

        public Where(IBoolable condition)
        {
            this.condition = condition;
        }

        public bool GetValue()
        {
            return condition.ToBool();
        }
    }
}
