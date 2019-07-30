using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class TimeFromDayMonth : DefaultTimeable
    {
        private INumerable day;
        private decimal month;


        public TimeFromDayMonth(INumerable day, decimal month)
        {
            this.day = day;
            this.month = month;
        }

        public override DateTime ToTime()
        {
            decimal day2 = decimal.Truncate(day.ToNumber());
            decimal year2 = GetYear();
            TimeValidator.ValidateDay(day2, month, year2);
            return new DateTime((int)year2, (int)month, (int)day2, 0, 0, 0);
        }
        
        private decimal GetYear()
        {
            return DateTime.Now.Year;
        }


        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return GetYear();
                case TimeVariableType.Month:
                    return month;
                case TimeVariableType.Day:
                    return day.ToNumber();
                case TimeVariableType.WeekDay:
                    return (decimal)ToTime().DayOfWeek;
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
