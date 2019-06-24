﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location
{
    class Exist : NamedVariable, IBoolable
    {
        public Exist()
        {
            name = "exist";
        }

        public bool ToBool()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");
            return FileInnerVariable.Exist(file);
        }

        public decimal ToNumber()
        {
            return ToBool() ? 1 : 0;
        }

        public override string ToString()
        {
            return ToBool() ? "1" : "0";
        }
    }
}
