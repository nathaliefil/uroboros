using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.time
{
    class FuncTomorrow : DefaultTimeable
    {
        public FuncTomorrow()
        {
        }

        public override DateTime ToTime()
        {
            return DateTime.Now.AddDays(1);
        }
    }
}
