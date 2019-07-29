using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class TimeFromDate : DefaultTimeable
    {
        private INumerable day;
        private int month;
        private INumerable year;


        public TimeFromDate(INumerable day, decimal month, INumerable year)
        {
            this.day = day;
            this.month = (int)month;
            this.year = year;
        }

        public override DateTime ToTime()
        {
            int day2 = (int)day.ToNumber();
            int year2 = (int)year.ToNumber();
            TimeValidator.ValidateYear(year2);
            TimeValidator.ValidateDay(day2, month, year2);
            return new DateTime(year2, month, day2, 0, 0, 0);
        }
    }
}
