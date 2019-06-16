using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.variables
{
    class Directories : NamedVariable, IListable
    {
        public Directories()
        {
            name = "directories";
        }

        public List<string> ToList()
        {
            string location = RuntimeVariables.GetInstance().GetValueString("location");
            return Directory.GetDirectories(location).ToList();
        }
    }
}
