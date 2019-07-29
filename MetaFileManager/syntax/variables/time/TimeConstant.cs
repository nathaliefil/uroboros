using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.time
{
    class TimeConstant : DefaultTimeableWithClockConstant
    {
        private decimal day;
        private decimal month;
        private decimal year;
        private decimal dayOfWeek;
        DateTime value;


        public TimeConstant(decimal day, decimal month, decimal year,
            decimal hour, decimal minute, decimal second)
        {
            this.day = day;
            this.month = month;
            this.year = year;
            this.hour = (int)hour;
            this.minute = (int)minute;
            this.second = (int)second;

            NormalizeClockVariables();
            TimeValidator.ValidateDate((int)this.day, (int)this.month, (int)this.year);
            AddForwardDays();
        }

        private void AddForwardDays()
        {
            value = new DateTime((int)year, (int)month, (int)day, hour, minute, second);
            if (daysForward != 0)
            {
                value.AddDays(daysForward);
                year = value.Year;
                month = value.Month;
                day = value.Day;
            }
            dayOfWeek = (decimal)value.DayOfWeek;
        }

        public override DateTime ToTime()
        {
            return value;
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return year;
                case TimeVariableType.Month:
                    return month;
                case TimeVariableType.Day:
                    return day;
                case TimeVariableType.WeekDay:
                    return (decimal)value.DayOfWeek;
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
