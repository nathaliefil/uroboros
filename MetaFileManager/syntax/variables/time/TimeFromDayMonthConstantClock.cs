using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class TimeFromDayMonthConstantClock : DefaultTimeableWithClockConstant
    {
        private INumerable day;
        private int month;


        public TimeFromDayMonthConstantClock(INumerable day, decimal month,
            decimal hour, decimal minute, decimal second)
        {
            this.day = day;
            this.month = (int)month;
            this.hour = (int)hour;
            this.minute = (int)minute;
            this.second = (int)second;

            NormalizeClockVariables();
        }

        public override DateTime ToTime()
        {
            int day2 = (int)day.ToNumber();
            int year2 = DateTime.Now.Year;
            TimeValidator.ValidateDay(day2, month, year2);

            DateTime time = new DateTime(year2, month, day2, hour, minute, second);
            if (daysForward != 0)
                time.AddDays(daysForward);

            return time;
        }
    }
}
