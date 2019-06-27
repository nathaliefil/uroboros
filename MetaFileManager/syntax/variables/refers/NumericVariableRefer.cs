using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
{
    class NumericVariableRefer : DefaultNumerable
    {
        private string name;

        public NumericVariableRefer(string name)
        {
            this.name = name;
        }

        public override decimal ToNumber()
        {
            return RuntimeVariables.GetInstance().GetValueNumber(name);
        }
    }
}
