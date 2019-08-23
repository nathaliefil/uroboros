using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.var
{
    class TimeElementDeclaration : ICommand
    {
        private string name;
        private TimeVariableType type;
        private INumerable value;

        public TimeElementDeclaration(string name, TimeVariableType type, INumerable value)
        {
            this.name = name;
            this.type = type;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().SetElementOfTime(name, value.ToNumber(), type);
        }
    }
}
