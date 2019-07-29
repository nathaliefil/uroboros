using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class TimeFromDateFunction : DefaultTimeable
    {
        private INumerable day;
        private INumerable month;
        private INumerable year;

        public TimeFromDateFunction(INumerable day, INumerable month, INumerable year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        public override DateTime ToTime()
        {
            return new DateTime((int)year.ToNumber(), (int)month.ToNumber(), (int)day.ToNumber(), 0, 0, 0);
        }
    }
}
