using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
{
    class ListElementRefer : DefaultStringable
    {
        private string name;
        private INumerable index;

        public ListElementRefer(string name, INumerable index)
        {
            this.name = name;
            this.index = index;
        }

        public override string ToString()
        {
            return RuntimeVariables.GetInstance().GetListElement(name, (int)index.ToNumber());
        }
    }
}
