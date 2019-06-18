using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric.abstracts
{
    abstract class INumericFunction : DefaultToListMethod, INumerable
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
