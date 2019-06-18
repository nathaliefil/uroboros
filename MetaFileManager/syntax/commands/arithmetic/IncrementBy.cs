using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.arithmetic
{
    class IncrementBy : ICommand
    {
        string variable;
        INumerable value;

        public IncrementBy(string variable, INumerable value)
        {
            this.variable = variable;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().IncrementBy(variable, value.ToNumber());
        }
    }
}
