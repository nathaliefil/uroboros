using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.abstracts
{
    abstract class NamedNumerable : NamedStringable, IStringable
    {
        public virtual decimal ToNumber()
        {
            return 0;
        }

        public override string ToString()
        {
            return ToNumber().ToString();
        }
    }
}
