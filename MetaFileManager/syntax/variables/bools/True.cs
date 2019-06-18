using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.bools
{
    class True : NamedVariable, IBoolable
    {
        public True()
        {
            name = "true";
        }

        public bool ToBool()
        {
            return true;
        }

        public decimal ToNumber()
        {
            return 1;
        }

        public override string ToString()
        {
            return "1";
        }
    }
}
