using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.constants
{
    class ListedListables : DefaultListable
    {
        private List<IListable> values;

        public ListedListables(List<IListable> values)
        {
            this.values = values;
        }

        public override List<string> ToList()
        {
            List<string> result = new List<string>();

            foreach (IListable list in values)
                result.AddRange(list.ToList());

            return result;
        }
    }
}
