using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.list
{
    class AddString : ICommand
    {
        private string name;
        private IStringable value;

        public AddString(string name, IStringable value)
        {
            this.name = name;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Add(name, value.ToString());
        }
    }
}
