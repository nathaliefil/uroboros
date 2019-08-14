using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_file
{
    class Path : NamedStringable
    {
        public Path()
        {
            name = "path";
        }

        public override string ToString()
        {
            return RuntimeVariables.GetInstance().GetLocation();
        }
    }
}
