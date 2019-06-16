using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.variables
{
    class Files : NamedVariable, IListable
    {
        public Files()
        {
            name = "files";
        }

        public List<string> ToList()
        {
            string location = RuntimeVariables.GetInstance().GetValueString("location");
            return Directory.GetFiles(location).ToList();
        }
    }
}
