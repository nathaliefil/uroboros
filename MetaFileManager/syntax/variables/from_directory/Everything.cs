using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables
{
    class Everything : NamedListable
    {
        public Everything()
        {
            name = "everything";
        }

        public override List<string> ToList()
        {
            List<string> everything = RuntimeVariables.GetInstance().GetValueList("directories");
            everything.AddRange(RuntimeVariables.GetInstance().GetValueList("files"));
            return everything;
        }
    }
}
