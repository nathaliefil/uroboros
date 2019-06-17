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
            string location = RuntimeVariables.GetInstance().GetValueString("location");
            List<string> everything = Directory.GetDirectories(location).ToList();
            everything.AddRange(Directory.GetFiles(location).ToList());
            return everything;
        }
    }
}
