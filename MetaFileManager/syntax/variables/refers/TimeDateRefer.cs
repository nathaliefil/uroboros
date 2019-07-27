using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
{
    class TimeDateRefer : DefaultStringable
    {
        private string name;

        public TimeDateRefer(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return RuntimeVariables.GetInstance().GetTimeDate(name);
        }
    }
}
