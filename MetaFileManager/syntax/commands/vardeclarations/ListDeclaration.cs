using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.commands
{
    class ListDeclaration : ICommand
    {
        private string name;
        private IListable value;

        ListDeclaration(string name, IListable value)
        {
            this.name = name;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Actualize(name, value.ToList());
        }
    }
}
