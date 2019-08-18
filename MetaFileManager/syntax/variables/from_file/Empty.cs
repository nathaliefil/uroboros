﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_file
{
    class Empty : NamedBoolable
    {
        public Empty()
        {
            name = "empty";
        }

        public override bool ToBool()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");
            return FileInnerVariable.Empty(file);
        }
    }
}
