using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class TimeFromDateClock : DefaultTimeable
    {
        private INumerable day;
        private decimal month;
        private INumerable year;
        private INumerable hour;
        private INumerable minute;
        private INumerable second;


        public TimeFromDateClock(INumerable day, decimal month, INumerable year, 
            INumerable hour, INumerable minute, INumerable second)
        {
            this.day = day;
            this.month = month;
            this.year = year;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public override DateTime ToTime()
        {
            decimal day2 = decimal.Truncate(day.ToNumber());
            decimal year2 = decimal.Truncate(year.ToNumber());
            TimeValidator.ValidateDate(day2, month, year2);

            decimal hour2 = decimal.Truncate(hour.ToNumber());
            decimal minute2 = decimal.Truncate(minute.ToNumber());
            decimal second2 = decimal.Truncate(second.ToNumber());

            return TimeCompiler.CreateDate((int)year2, (int)month, (int)day2, (int)hour2, (int)minute2, (int)second2);
        }


    }
}
