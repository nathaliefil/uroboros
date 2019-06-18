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

        public override List<string> ToList()
        {
            string location = RuntimeVariables.GetInstance().GetValueString("location");
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
