using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
{
    class BoolVariableRefer : DefaultBoolable
    {
        private string name;

        public BoolVariableRefer(string name)
        {
            this.name = name;
        }

        public override bool ToBool()
        {
            return RuntimeVariables.GetInstance().GetValueBool(name);
        }
    }
}
