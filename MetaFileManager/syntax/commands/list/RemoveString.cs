using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.list
{
    class RemoveString : ICommand
    {
        private string name;
        private IStringable value;

        public RemoveString(string name, IStringable value)
        {
            this.name = name;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Remove(name, value.ToString());
        }
    }
}
