using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.time
{
    class YearNow : IYear
    {
        public YearNow()
        {
        }

        public decimal ToYear()
        {
            return DateTime.Now.Year;
        }
    }
}
