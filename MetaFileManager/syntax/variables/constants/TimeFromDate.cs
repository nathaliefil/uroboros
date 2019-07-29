using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class TimeFromDate : DefaultTimeable
    {
        private INumerable day;
        private decimal month;
        private INumerable year;

        public TimeFromDate(INumerable day, decimal month, INumerable year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        public override DateTime ToTime()
        {
            int day2 = (int)day.ToNumber();
            int month2 = (int)month;
            int year2 = (int)year.ToNumber();
            TimeValidator.ValidateDate(day2, month2, year2);
            return new DateTime(year2, month2, day2, 0, 0, 0);
        }
    }
}
