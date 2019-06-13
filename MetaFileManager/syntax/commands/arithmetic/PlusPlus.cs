using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.arithmetic
{
    class PlusPlus : ICommand
    {
        string variable;

        public PlusPlus(string variable)
        {
            this.variable = variable;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().PlusPlus(variable);
        }
    }
}
