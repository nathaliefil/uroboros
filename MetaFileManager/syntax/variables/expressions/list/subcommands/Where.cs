using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.variables.expressions.list.subcommands
{
    class Where : ISubcommand
    {
        private BoolExpression condition;

        public Where(BoolExpression condition)
        {
            this.condition = condition;
        }

        public bool GetValue()
        {
            return condition.ToBool();
        }
    }
}
