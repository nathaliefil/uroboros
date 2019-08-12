using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.structures
{
    class IfOpenning : BracketOn, IBoolable
    {
        private IBoolable condition;

        public IfOpenning(IBoolable condition, int commandNumber)
        {
            this.condition = condition;
            this.commandNumber = commandNumber;
        }

        public bool ToBool()
        {
            return condition.ToBool();
        }
    }
}
