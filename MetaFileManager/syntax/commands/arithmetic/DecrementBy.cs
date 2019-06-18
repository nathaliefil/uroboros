using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.commands.arithmetic
{
    class DecrementBy : ICommand
    {
        string variable;
        INumerable value;

        public DecrementBy(string variable, INumerable value)
        {
            this.variable = variable;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().DecrementBy(variable, value.ToNumber());
        }
    }
}
