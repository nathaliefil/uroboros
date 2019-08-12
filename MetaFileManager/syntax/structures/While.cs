using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.structures.abstracts;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.structures
{
    class While : Structure, ILoopingStructure
    {
        private IBoolable condition;

        public While(IBoolable condition, int commandNumber)
        {
            this.condition = condition;
            this.commandNumber = commandNumber;
        }

        public override bool HasNext()
        {
            return condition.ToBool();
        }
    }
}
