using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands
{
    class StringDeclaration : ICommand
    {
        private string name;
        private IStringable value;

        public StringDeclaration(string name, IStringable value)
        {
            this.name = name;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Actualize(name, value.ToString());
        }
    }
}
