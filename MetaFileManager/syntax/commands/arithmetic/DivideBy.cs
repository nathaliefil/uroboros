using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.arithmetic
{
    class DivideBy : ICommand
    {
        private string variable;
        private INumerable value;

        public DivideBy(string variable, INumerable value)
        {
            this.variable = variable;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().DivideBy(variable, value.ToNumber());
        }
    }
}
