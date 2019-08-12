using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.structures.abstracts;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.structures
{
    class NumericLoop : Structure, ILoopingStructure
    {
        private int repeats;

        private decimal previousIndex;

        public NumericLoop(int repeats, int commandNumber)
        {
            this.repeats = repeats;
            this.commandNumber = commandNumber;

            previousIndex = RuntimeVariables.GetInstance().GetValueNumber("index");
        }

        public override bool HasNext()
        {
            if (repeats == 0)
            {
                RuntimeVariables.GetInstance().Actualize("index", previousIndex);
                return false;
            }
            else
            {
                repeats--;

                RuntimeVariables.GetInstance().IncrementBy("index", 1);
                return true;
            }
        }
    }
}
