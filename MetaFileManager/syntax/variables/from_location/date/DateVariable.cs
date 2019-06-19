using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.from_location.date
{
    class DateVariable : NamedVariable, INumerable
    {
        private bool modification;
        private DateVariableType type;

        public DateVariable(string name, bool modification, DateVariableType type)
        {
            this.name = name;
            this.modification = modification;
            this.type = type;
        }

        public decimal ToNumber()
        {
            string address = RuntimeVariables.GetInstance().GetValueString("location") +
                "//" + RuntimeVariables.GetInstance().GetValueString("this");

            try
            {
                DateTime time = modification ? System.IO.File.GetLastWriteTime(@address) 
                    : System.IO.File.GetCreationTime(@address);
                return DateExtractor.GetVariable(type, time);
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
