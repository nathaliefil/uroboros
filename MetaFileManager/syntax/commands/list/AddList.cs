using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.list
{
    class AddList : ICommand
    {
        private string name;
        private IListable values;

        public AddList(string name, IListable values)
        {
            this.name = name;
            this.values = values;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Add(name, values.ToList());
        }
    }
}
