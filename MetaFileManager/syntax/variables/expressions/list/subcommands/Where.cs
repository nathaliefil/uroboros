using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.expressions.list.subcommands
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
