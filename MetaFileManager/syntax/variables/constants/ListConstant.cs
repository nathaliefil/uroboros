using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class ListConstant : IListable
    {
        private List<string> values;

        public ListConstant(List<string> values)
        {
            this.values = values;
        }

        public List<string> ToList()
        {
            return values;
        }
    }
}
