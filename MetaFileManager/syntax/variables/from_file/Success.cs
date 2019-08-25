using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_file
{
    class Success : NamedBoolable
    {
        private bool value;

        public Success()
        {
            name = "success";
            value = false;
        }

        public override bool ToBool()
        {
            return value;
        }

        public void Succeed()
        {
            value = true;
        }

        public void Failed()
        {
            value = false;
        }
    }
}
