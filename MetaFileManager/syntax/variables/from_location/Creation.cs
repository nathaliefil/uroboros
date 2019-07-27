using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location.date
{
    class Creation : NamedTimeable
    {
        public Creation()
        {
            name = "creation";
        }

        public override DateTime ToTime()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");

            try
            {
                return FileInnerVariable.GetCreation(file);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
    }
}
