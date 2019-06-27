using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.abstracts
{
    abstract class DefaultListable : IListable
    {
        public virtual List<string> ToList()
        {
            return new List<string>();
        }
    }
}
