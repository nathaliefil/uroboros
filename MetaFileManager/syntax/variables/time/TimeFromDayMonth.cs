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
        private int month;


        public TimeFromDayMonth(INumerable day, decimal month)
        {
            this.day = day;
            this.month = (int)month;
        }

        public override DateTime ToTime()
        {
            int day2 = (int)day.ToNumber();
            int year2 = DateTime.Now.Year;
            TimeValidator.ValidateDay(day2, month, year2);
            return new DateTime(year2, month, day2, 0, 0, 0);
        }
    }
}
