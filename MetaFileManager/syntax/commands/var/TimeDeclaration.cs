using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.var
{
    class TimeDeclaration : ICommand
    {
        private string name;
        private ITimeable value;

        public TimeDeclaration(string name, ITimeable value)
        {
            this.name = name;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Actualize(name, value.ToTime());
        }
    }
}
