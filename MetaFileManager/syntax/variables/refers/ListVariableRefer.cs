using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.variables.refers
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
