using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables
{
    class Files : NamedListable
    {
        public Files()
        {
            name = "files";
        }

        public override List<string> ToList()
        {
            string location = RuntimeVariables.GetInstance().GetWholeLocation();
            int length = location.Length;
            List<string> list = ((Directory.GetFiles(location)).Select(s => s.Substring(length))).ToList();
            List<string> newlist = new List<string>();

            foreach (string l in list)
            {
                if (l.StartsWith("\\"))
                    newlist.Add(l.Substring(1));
                else
                    newlist.Add(l);
            }

            return newlist;
        }
    }
}
