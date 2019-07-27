using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
{
    class TimeElementRefer : DefaultNumerable
    {
        private string name;
        private TimeVariableType type;

        public TimeElementRefer(string name, TimeVariableType type)
        {
            this.name = name;
            this.type = type;
        }

        public override decimal ToNumber()
        {
            return RuntimeVariables.GetInstance().GetTimeElement(name, type);
        }
    }
}
