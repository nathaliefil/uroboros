using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.time
{
    class YearConstant : IYear
    {
        private decimal year;

        public YearConstant(decimal year)
        {
            this.year = year;
        }

        public decimal ToYear()
        {
            return year;
        }
    }
}
