using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands
{
    class BoolDeclaration : ICommand
    {
        private string name;
        private IBoolable value;

        public BoolDeclaration(string name, IBoolable value)
        {
            this.name = name;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Actualize(name, value.ToBool());
        }
    }
}
