using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location
{
    class Name : NamedVariable, IStringable
    {
        public Name()
        {
            name = "name";
        }

        public override string ToString()
        {
            string thiss = RuntimeVariables.GetInstance().GetValueString("this");
            int position = thiss.LastIndexOf("\\");
            if (position > -1)
                thiss = thiss.Substring(position);

            if (thiss.LastIndexOf('.') == -1)
                return thiss;
            else
                return thiss.Substring(0, thiss.LastIndexOf('.'));
        }
    }
}
