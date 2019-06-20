using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.from_location
{
    class Size : NamedVariable, INumerable
    {
        public Size()
        {
            this.name = "size";
        }

        public decimal ToNumber()
        {
            string thiss = RuntimeVariables.GetInstance().GetValueString("this");
            string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + thiss;

            try
            {
                return (decimal)(new System.IO.FileInfo(location).Length);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return ToNumber().ToString();
        }
    }
}
