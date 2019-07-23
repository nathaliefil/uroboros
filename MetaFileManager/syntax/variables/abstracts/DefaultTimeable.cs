using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.abstracts
{
    abstract class DefaultTimeable : DefaultStringable, ITimeable
    {
        public virtual DateTime ToTime()
        {
            return DateTime.MinValue;
        }

        public override string ToString()
        {
            return ""; // todo
        }
    }
}
