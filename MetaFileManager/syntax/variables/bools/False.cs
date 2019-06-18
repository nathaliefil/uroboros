using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.bools
{
    class False : NamedVariable, IBoolable
    {
        public False()
        {
            name = "true";
        }

        public bool ToBool()
        {
            return false;
        }

        public decimal ToNumber()
        {
            return 0;
        }

        public override string ToString()
        {
            return "0";
        }
    }
}
