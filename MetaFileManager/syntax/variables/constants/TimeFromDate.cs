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
            return new DateTime((int)year.ToNumber(), (int)month, (int)day.ToNumber(), 0, 0, 0);
        }
    }
}
