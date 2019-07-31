using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.time
{
    class ClockEmpty : IClock
    {
        public ClockEmpty()
        {
        }

        public decimal ToHour()
        {
            return 0;
        }
        public decimal ToMinute()
        {
            return 0;
        }
        public decimal ToSecond()
        {
            return 0;
        }
    }
}
