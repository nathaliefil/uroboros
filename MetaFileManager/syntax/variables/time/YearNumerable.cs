using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class YearNumerable : IYear
    {
        INumerable year;

        public YearNumerable(INumerable year)
        {
            this.year = year;
        }

        public decimal ToYear()
        {
            return year.ToNumber();
        }
    }
}
