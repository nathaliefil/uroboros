using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands
{
    class BoolDeclaration : ICommand
    {
        private string name;
        private BoolExpression value;

        BoolDeclaration(string name, BoolExpression value)
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
