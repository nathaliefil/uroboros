using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class ClockNumerables : IClock
    {
        INumerable hour;
        INumerable minute;
        INumerable second;

        public ClockNumerables(INumerable hour, INumerable minute, INumerable second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public decimal ToHour()
        {
            return hour.ToNumber();
        }
        public decimal ToMinute()
        {
            return minute.ToNumber();
        }
        public decimal ToSecond()
        {
            return second.ToNumber();
        }
    }
}
