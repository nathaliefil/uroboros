using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
{
    class TimeVariableRefer : DefaultTimeable
    {
        private string name;

        public TimeVariableRefer(string name)
        {
            this.name = name;
        }

        public override DateTime ToTime()
        {
            return RuntimeVariables.GetInstance().GetValueTime(name);
        }
    }
}
