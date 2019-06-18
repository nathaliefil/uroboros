using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
{
    class ListVariableRefer : IListable
    {
        private string name;

        public ListVariableRefer(string name)
        {
            this.name = name;
        }

        public List<string> ToList()
        {
            return RuntimeVariables.GetInstance().GetValueList(name);
        }
    }
}
