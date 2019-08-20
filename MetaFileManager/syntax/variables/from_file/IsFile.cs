using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_file
{
    class IsFile : NamedBoolable
    {
        public IsFile()
        {
            name = "isfile";
        }

        public override bool ToBool()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");
            return !FileValidator.IsDirectory(file);
        }
    }
}
