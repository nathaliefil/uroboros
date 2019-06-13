using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.arithmetic
{
    class MinusMinus : ICommand
    {
        string variable;

        public MinusMinus(string variable)
        {
            this.variable = variable;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().MinusMinus(variable);
        }
    }
}
