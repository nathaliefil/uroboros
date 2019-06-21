using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location.date
{
    class Creation : NamedVariable, IStringable
    {
        public Creation()
        {
            name = "creation";
        }

        public override string ToString()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");

            try
            {
                DateTime time = FileInnerVariable.GetCreation(file);
                return DateBuilder.Build(time);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
