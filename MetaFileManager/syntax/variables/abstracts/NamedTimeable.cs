using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.variables.abstracts
{
    class NamedTimeable : NamedStringable, ITimeable
    {
        public virtual DateTime ToTime()
        {
            return DateTime.MinValue;
        }

        public decimal ToTimeVariable(TimeVariableType type)
        {
            return DateExtractor.GetVariable(ToTime(), type);
        }

        public override string ToString()
        {
            return DateExtractor.ToString(ToTime());
        }

        public string ToDate()
        {
            return DateExtractor.ToDate(ToTime());
        }

        public string ToClock()
        {
            return DateExtractor.ToClock(ToTime());
        }
    }
}
