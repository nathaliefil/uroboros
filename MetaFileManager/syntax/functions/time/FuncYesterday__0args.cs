using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.time
{
    class FuncYesterday__0args : DefaultTimeable
    {
        public FuncYesterday__0args()
        {
        }

        public override DateTime ToTime()
        {
            DateTime s = DateTime.Now.AddDays(-1);
            TimeSpan ts = new TimeSpan(0, 0, 0);
            s = s.Date + ts;
            return s;
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return DateTime.Now.AddDays(-1).Year;
                case TimeVariableType.Month:
                    return DateTime.Now.AddDays(-1).Month;
                case TimeVariableType.Day:
                    return DateTime.Now.AddDays(-1).Day;
                case TimeVariableType.WeekDay:
                    return DateExtractor.GetWeekDay(ToTime());
                case TimeVariableType.Hour:
                    return 0;
                case TimeVariableType.Minute:
                    return 0;
                case TimeVariableType.Second:
                    return 0;
            }
            return 0;
        }
    }
}
