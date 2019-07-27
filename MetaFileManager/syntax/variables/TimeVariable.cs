using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class TimeVariable : NamedTimeable
    {
        private DateTime value;

        public TimeVariable(string name, DateTime value)
        {
            this.name = name;
            this.value = value;
        }

        public override DateTime ToTime()
        {
            return value;
        }

        public void SetValue(DateTime dec)
        {
            value = dec;
        }
    }
}
