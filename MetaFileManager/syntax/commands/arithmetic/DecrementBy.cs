using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.arithmetic
{
    class DecrementBy : ICommand
    {
        private string variable;
        private INumerable value;

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
