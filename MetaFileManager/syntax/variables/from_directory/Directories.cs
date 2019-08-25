using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables
{
    class Directories : NamedListable
    {
        public Directories()
        {
            name = "directories";
        }

        public override List<string> ToList()
        {
            string location = RuntimeVariables.GetInstance().GetWholeLocation();
            int length = location.Length;
            List<string> list = ((Directory.GetDirectories(location)).Select(s => s.Substring(length))).ToList();
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
