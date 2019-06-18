﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location
{
    class Extension : NamedVariable, IStringable
    {
        public Extension()
        {
            name = "extension";
        }

        public override string ToString()
        {
            string thiss = RuntimeVariables.GetInstance().GetValueString("this");
            if (thiss.LastIndexOf('.') == -1)
                return "";
            else
                return thiss.Substring(thiss.LastIndexOf('.')+1);
        }
    }
}
