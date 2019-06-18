using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.variables
{
    class Everything : NamedVariable, IListable
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
