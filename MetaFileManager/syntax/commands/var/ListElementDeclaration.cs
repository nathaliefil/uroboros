using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.var
{
    class ListElementDeclaration : ICommand
    {
        private string name;
        private IStringable value;
        private INumerable index;

        public ListElementDeclaration(string name, IStringable value, INumerable index)
        {
            this.name = name;
            this.value = value;
            this.index = index;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().SetElementAtIndex(name, value.ToString(), (int)index.ToNumber());
        }
    }
}
