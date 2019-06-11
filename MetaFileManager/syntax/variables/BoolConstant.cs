using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables
{
    class BoolConstant : Variable, IBoolable, INumerable, IStringable
    {
        private bool value;

        public BoolConstant(bool value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            if (value)
                return "true";
            else
                return "false";
        }

        public bool ToBool()
        {
            return value;
        }

        public decimal ToNumber()
        {
            if (value)
                return 1;
            else
                return 0;
        }

    }
}
