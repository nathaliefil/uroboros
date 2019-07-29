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
        private int month;
        private INumerable year;
        private INumerable hour;
        private INumerable minute;
        private INumerable second;


        public TimeFromDateClock(INumerable day, decimal month, INumerable year, 
            INumerable hour, INumerable minute, INumerable second)
        {
            this.day = day;
            this.month = (int)month;
            this.year = year;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public override DateTime ToTime()
        {
            int day2 = (int)day.ToNumber();
            int year2 = (int)year.ToNumber();
            TimeValidator.ValidateDate(day2, month, year2);

            int hour2 = (int)hour.ToNumber();
            int minute2 = (int)minute.ToNumber();
            int second2 = (int)second.ToNumber();

            return TimeCompiler.CreateDate(year2, month, day2, hour2, minute2, second2);
        }
    }
}
