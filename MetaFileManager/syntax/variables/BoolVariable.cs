using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class BoolVariable : NamedBoolable
    {
        private bool value;

        public BoolVariable(string name, bool value)
        {
            this.name = name;
            this.value = value;
        }

        public override bool ToBool()
        {
            return value;
        }

        public void SetValue(bool b)
        {
            value = b;
        }
    }
}
