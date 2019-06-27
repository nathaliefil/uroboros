using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.constants
{
    class ListedStringables : DefaultListable
    {
        private List<IStringable> values;

        public ListedStringables(List<IStringable> values)
        {
            this.values = values;
        }

        public override List<string> ToList()
        {
            return values.Select(v => v.ToString()).ToList();
        }
    }
}
