using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.abstracts
{
    abstract class DefaultStringable : DefaultListable, IStringable
    {
        public override string ToString()
        {
            return "";
        }

        public override List<string> ToList()
        {
            return new List<string>{ ToString() };
        }
    }
}
