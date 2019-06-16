using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.variables.refers
{
    class StringVariableRefer : IStringable
    {
        private string name;

        public StringVariableRefer(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return RuntimeVariables.GetInstance().GetValueString(name);
        }
    }
}
