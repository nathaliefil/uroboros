using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.abstracts
{
    abstract class DefaultBoolable : DefaultNumerable, IBoolable
    {
        public virtual bool ToBool()
        {
            return false;
        }

        public override decimal ToNumber()
        {
            return ToBool() ? 1 : 0;
        }
    }
}
