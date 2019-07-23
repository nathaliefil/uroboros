using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.from_location.date
{
    class DateVariableNumeric : NamedNumerable
    {
        private bool modification;
        private DateVariableType type;

        public DateVariableNumeric(string name, bool modification, DateVariableType type)
        {
            this.name = name;
            this.modification = modification;
            this.type = type;
        }

        public override decimal ToNumber()
        {
            string file = RuntimeVariables.GetInstance().GetValueString("this");

            try
            {
                DateTime time = modification ? FileInnerVariable.GetModification(file)
                    : FileInnerVariable.GetCreation(file);
                return DateExtractor.GetVariableNumeric(type, time);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
