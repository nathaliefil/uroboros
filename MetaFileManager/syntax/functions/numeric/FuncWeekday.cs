using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncWeekday : DefaultNumerable
    {
        private ITimeable arg0;

        public FuncWeekday(ITimeable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return (decimal)arg0.ToTime().DayOfWeek;
        }
    }
}
