using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.arithmetic
{
    class MultiplyBy : ICommand
    {
        private string variable;
        private INumerable value;

        public MultiplyBy(string variable, INumerable value)
        {
            this.variable = variable;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().MultiplyBy(variable, value.ToNumber());
        }
    }
}
