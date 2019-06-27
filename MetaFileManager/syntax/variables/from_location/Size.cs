using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location
{
    class Size : NamedNumerable
    {
        public Size()
        {
            this.name = "size";
        }

        public override decimal ToNumber()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");
            return FileInnerVariable.GetSize(file);
        }
    }
}
