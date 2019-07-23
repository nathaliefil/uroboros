using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.from_location.date
{
    class DateVariableString : NamedStringable
    {
        private bool modification;
        private DateVariableType type;

        public DateVariableString(string name, bool modification, DateVariableType type)
        {
            this.name = name;
            this.modification = modification;
            this.type = type;
        }

        public override string ToString()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");

            try
            {
                DateTime time = modification ? FileInnerVariable.GetModification(file)
                    : FileInnerVariable.GetCreation(file);
                return DateExtractor.GetVariableString(type, time);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
