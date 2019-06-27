using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class EmptyList : DefaultListable
    {
        public EmptyList()
        {
        }

        public override List<string> ToList()
        {
            return new List<string>();
        }
    }
}
