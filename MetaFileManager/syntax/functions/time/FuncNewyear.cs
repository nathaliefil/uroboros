using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.time
{
    class FuncNewyear : DefaultTimeable
    {
        private INumerable arg0;

        public FuncNewyear(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override DateTime ToTime()
        {
            int year = (int)arg0.ToNumber();
            TimeValidator.ValidateYear(year);

            return new DateTime(year, 1, 1, 0, 0, 0);
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return arg0.ToNumber();
                case TimeVariableType.Month:
                    return 1;
                case TimeVariableType.Day:
                    return 1;
                case TimeVariableType.WeekDay:
                    return (decimal)ToTime().DayOfWeek;
                case TimeVariableType.Hour:
                    return 0;
                case TimeVariableType.Minute:
                    return 0;
                case TimeVariableType.Second:
                    return 0;
            }
            return 0;
        }
    }
}
