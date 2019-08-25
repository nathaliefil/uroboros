using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_file;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.bools
{
    class FuncSameClocks : DefaultBoolable
    {
        private ITimeable arg0;
        private ITimeable arg1;

        public FuncSameClocks(ITimeable arg0, ITimeable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override bool ToBool()
        {
            bool sameHour = arg0.ToTimeVariable(TimeVariableType.Hour) == arg1.ToTimeVariable(TimeVariableType.Hour);
            bool sameMinute = arg0.ToTimeVariable(TimeVariableType.Minute) == arg1.ToTimeVariable(TimeVariableType.Minute);
            bool sameSecond = arg0.ToTimeVariable(TimeVariableType.Second) == arg1.ToTimeVariable(TimeVariableType.Second);

            return sameHour && sameMinute && sameSecond;
        }
    }
}
