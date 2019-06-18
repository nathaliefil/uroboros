using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.abstracts
{
    class NullVariable : IBoolable
    {
        /*

        this variable is never used runtime
        it's purpose is to inform interpreter
        that expression interpretation went wrong

            */
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
            return "";
        }

        public List<string> ToList()
        {
            return null;
        }

    }
}
