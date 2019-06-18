using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using System.IO;

namespace DivineScript.syntax.variables.from_location
{
    class Fullname : NamedVariable, IStringable
    {
        public Fullname()
        {
            name = "fullname";
        }

        public override string ToString()
        {
            string thiss = RuntimeVariables.GetInstance().GetValueString("this");
            int position = thiss.LastIndexOf("\\");
            if (position > -1)
                thiss = thiss.Substring(position);

            return thiss;
        }
    }
}
