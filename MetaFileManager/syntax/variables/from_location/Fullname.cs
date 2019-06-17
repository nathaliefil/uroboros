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
            if (this.Equals(""))
            {
                return "";
            }
            string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + thiss;

            try
            {
                string value;

                if (FileValidator.IsDirectory(thiss))
                    value = Path.GetDirectoryName(@location);
                else
                    value = Path.GetFileName(@location);

                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
