using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.from_location.date
{
    class DateExtractor
    {
        public static decimal GetVariable(DateVariableType type, DateTime time)
        {
            switch (type)
            {
                case DateVariableType.Year:
                    return time.Year;
                case DateVariableType.Month:
                    return time.Month;
                case DateVariableType.WeekDay:
                    return (decimal)time.DayOfWeek;
                case DateVariableType.Day:
                    return time.Day;
                case DateVariableType.Hour:
                    return time.Hour;
                case DateVariableType.Minute:
                    return time.Minute;
                case DateVariableType.Second:
                    return time.Second;
            }
            return 0;
        }
    }
}
