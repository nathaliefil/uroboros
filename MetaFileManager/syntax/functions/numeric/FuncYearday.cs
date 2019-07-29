using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncYearday : DefaultNumerable
    {
        private ITimeable arg0;

        public FuncYearday(ITimeable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            return (decimal)arg0.ToTime().DayOfYear;
        }
    }
}
