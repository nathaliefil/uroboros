using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands
{
    class NumericDeclaration : ICommand
    {
        private string name;
        private NumericExpression value;

        NumericDeclaration(string name, NumericExpression value)
        {
            this.name = name;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.Actualize(name, value.ToNumber());
        }
    }
}
