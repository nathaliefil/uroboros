using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.arithmetic
{
    class ModuloBy : ICommand
    {
        private string variable;
        private INumerable value;

        public ModuloBy(string variable, INumerable value)
        {
            this.variable = variable;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().ModuloBy(variable, value.ToNumber());
        }
    }
}
