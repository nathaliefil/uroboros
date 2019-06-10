using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables
{
    class StringConstant : Variable, IStringable
    {
        private string value;

        StringConstant(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

    }
}
