using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.from_file
{
    class WholeLocation : NamedStringable
    {
        public WholeLocation()
        {
            name = "wholelocation";
        }

        public override string ToString()
        {
            return RuntimeVariables.GetInstance().GetWholeLocation();
        }
    }
}
