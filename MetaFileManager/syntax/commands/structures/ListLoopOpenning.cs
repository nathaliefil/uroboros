using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.structures
{
    class ListLoopOpenning : BracketOn, IListable
    {
        private IListable list;

        public ListLoopOpenning(IListable list, int commandNumber)
        {
            this.list = list;
            this.commandNumber = commandNumber;
        }

        public List<string> ToList()
        {
            return list.ToList();
        }
    }
}
