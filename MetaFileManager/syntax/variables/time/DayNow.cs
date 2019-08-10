using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.time
{
    class DayNow : IDay
    {
        public DayNow()
        {
        }

        public decimal ToDay()
        {
            return DateTime.Now.Day;
        }
    }
}
