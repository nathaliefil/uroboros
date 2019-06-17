using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.bools
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
