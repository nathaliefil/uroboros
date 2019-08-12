using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.structures.abstracts;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.structures
{
    class Inside: Structure, ILoopingStructure
    {
        private List<string> list;

        private decimal previousIndex;

        public Inside(List<string> list, int commandNumber)
        {
            this.list = list;
            this.commandNumber = commandNumber;

            previousIndex = RuntimeVariables.GetInstance().GetValueNumber("index");
        }

        public override bool HasNext()
        {
            if (list.Count == 0)
            {
                RuntimeVariables.GetInstance().Actualize("index", previousIndex);
                RuntimeVariables.GetInstance().RetreatLocation();

                return false;
            }
            else
            {
                string value = list[0];
                list.RemoveAt(0);

                RuntimeVariables.GetInstance().ReplaceLocationEnding(value);
                RuntimeVariables.GetInstance().IncrementBy("index", 1);

                return true;
            }
        }
    }
}
