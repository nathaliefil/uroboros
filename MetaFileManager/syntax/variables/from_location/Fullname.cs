using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location
{
    class Fullname : NamedVariable, IStringable
    {
        public Fullname()
        {
            name = "fullname";
        }

        public override string ToString()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");
            return FileInnerVariable.GetFullname(file);
        }
    }
}
