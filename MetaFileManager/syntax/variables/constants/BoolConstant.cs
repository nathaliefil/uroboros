using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class BoolConstant : DefaultBoolable
    {
        private bool value;

        public BoolConstant(bool value)
        {
            this.value = value;
        }

        public override bool ToBool()
        {
            return value;
        }
    }
}
