using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables
{
    class BoolVariable : NamedVariable, IBoolable, INumerable
    {
        private bool value;

        BoolVariable(string name, bool value)
        {
            this.name = name;
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

        public void SetValue(bool b)
        {
            value = b;
        }

    }
}
