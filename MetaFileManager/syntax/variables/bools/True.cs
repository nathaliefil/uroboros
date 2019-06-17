using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.bools
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

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
