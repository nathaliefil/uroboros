using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class DayNumerable : IDay
    {
        private INumerable day;

        public DayNumerable(INumerable day)
        {
            this.day = day;
        }

        public decimal ToDay()
        {
            return day.ToNumber();
        }
    }
}
