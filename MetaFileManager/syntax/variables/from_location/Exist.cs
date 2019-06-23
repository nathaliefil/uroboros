using System;
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
            string thiss = RuntimeVariables.GetInstance().GetValueString("this");
            string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + thiss;

            if (FileValidator.IsDirectory(thiss))
            {
                if (Directory.Exists(@location))
                    return true;
                else
                    return false;
            }
            else
            {
                if (File.Exists(@location))
                    return true;
                else
                    return false;
            }
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
