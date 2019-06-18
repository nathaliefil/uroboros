using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
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

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
