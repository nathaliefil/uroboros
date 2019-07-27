using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.refers
{
    class Now : NamedTimeable
    {
        public Now()
        {
            name = "now";
        }

        public override DateTime ToTime()
        {
            return DateTime.Now;
        }
    }
}
