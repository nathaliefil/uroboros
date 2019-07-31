using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.time
{
    class ClockConstant : IClock
    {
        decimal hour;
        decimal minute;
        decimal second;

        public ClockConstant(decimal hour, decimal minute, decimal second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public decimal ToHour()
        {
            return hour;
        }
        public decimal ToMinute()
        {
            return minute;
        }
        public decimal ToSecond()
        {
            return second;
        }
    }
}
