﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_file
{
    class Access : NamedTimeable
    {
        public Access()
        {
            name = "access";
        }

        public override DateTime ToTime()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");

            try
            {
                return FileInnerVariable.GetAccess(file);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
    }
}
