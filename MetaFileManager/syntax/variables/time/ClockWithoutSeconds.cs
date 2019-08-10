using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class ClockWithoutSeconds : IClock
    {
        private INumerable hour;
        private INumerable minute;

        public ClockWithoutSeconds(INumerable hour, INumerable minute)
        {
            this.hour = hour;
            this.minute = minute;
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
            return 0;
        }
    }
}
