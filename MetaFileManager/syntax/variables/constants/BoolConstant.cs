using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class BoolConstant : DefaultToListMethod, IBoolable
    {
        private bool value;

        public BoolConstant(bool value)
        {
            this.value = value;
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

        public override string ToString()
        {
            if (ToBool())
                return "1";
            else
                return "0";
        }
    }
}
