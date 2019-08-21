using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.time
{
    class DayConstant : IDay
    {
        private decimal day;

        public DayConstant(decimal day)
        {
            this.day = day;
        }

        public decimal ToDay()
        {
            return day;
        }
    }
}
