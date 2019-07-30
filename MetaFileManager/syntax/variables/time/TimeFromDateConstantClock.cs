using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class TimeFromDateConstantClock : DefaultTimeableWithClockConstant
    {
        private INumerable day;
        private decimal month;
        private INumerable year;


        public TimeFromDateConstantClock(INumerable day, decimal month, INumerable year,
            decimal hour, decimal minute, decimal second)
        {
            this.day = day;
            this.month = month;
            this.year = year;
            this.hour = hour;
            this.minute = minute;
            this.second = second;

            NormalizeClockVariables();
        }

        public override DateTime ToTime()
        {
            decimal day2 = decimal.Truncate(day.ToNumber());
            decimal year2 = decimal.Truncate(year.ToNumber());
            TimeValidator.ValidateDate(day2, month, year2);

            DateTime time = new DateTime((int)year2, (int)month, (int)day2, (int)hour, (int)minute, (int)second);
            if (daysForward != 0)
                time = time.AddDays((int)daysForward);

            return time;
        }


        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return year.ToNumber();
                case TimeVariableType.Month:
                    return month;
                case TimeVariableType.Day:
                    return day.ToNumber();
                case TimeVariableType.WeekDay:
                    return (decimal)ToTime().DayOfWeek;
                case TimeVariableType.Hour:
                    return hour;
                case TimeVariableType.Minute:
                    return minute;
                case TimeVariableType.Second:
                    return second;
            }
            return 0;
        }
    }
}
