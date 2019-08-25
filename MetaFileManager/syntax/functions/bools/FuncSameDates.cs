using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_file;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.bools
{
    class FuncSameDates : DefaultBoolable
    {
        private ITimeable arg0;
        private ITimeable arg1;

        public FuncSameDates(ITimeable arg0, ITimeable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override bool ToBool()
        {
            bool sameYear = arg0.ToTimeVariable(TimeVariableType.Year) == arg1.ToTimeVariable(TimeVariableType.Year);
            bool sameMonth = arg0.ToTimeVariable(TimeVariableType.Month) == arg1.ToTimeVariable(TimeVariableType.Month);
            bool sameDay = arg0.ToTimeVariable(TimeVariableType.Day) == arg1.ToTimeVariable(TimeVariableType.Day);

            return sameYear && sameMonth && sameDay;
        }
    }
}
