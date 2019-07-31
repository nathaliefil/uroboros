using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class Time : DefaultTimeable
    {
        private IClock clock;
        private IDay day;
        private decimal month;
        private IYear year;

        public Time(IClock clock, IDay day, decimal month, IYear year)
        {
            this.clock = clock;

            this.day = day;
            this.month = month;
            this.year = year;
        }

        public override DateTime ToTime()
        {
            int years = (int)year.ToYear();
            int months = (int)month;
            int days = (int)day.ToDay();
            int hours = (int)clock.ToHour();
            int minutes = (int)clock.ToMinute();
            int seconds = (int)clock.ToSecond();

            TimeValidator.ValidateDate(days, months, years);
            TimeValidator.ValidateClock(hours, minutes, seconds);

            return new DateTime(years, months, days, hours, minutes, seconds);
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return year.ToYear();
                case TimeVariableType.Month:
                    return month;
                case TimeVariableType.Day:
                    return day.ToDay();
                case TimeVariableType.WeekDay:
                    return (decimal)ToTime().DayOfWeek;
                case TimeVariableType.Hour:
                    return clock.ToHour();
                case TimeVariableType.Minute:
                    return clock.ToMinute();
                case TimeVariableType.Second:
                    return clock.ToSecond();
            }
            return 0;
        }
    }
}
