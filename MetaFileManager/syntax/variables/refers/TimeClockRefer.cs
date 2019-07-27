using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
{
    class TimeClockRefer : DefaultStringable
    {
        private string name;

        public TimeClockRefer(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return RuntimeVariables.GetInstance().GetTimeClock(name);
        }
    }
}
