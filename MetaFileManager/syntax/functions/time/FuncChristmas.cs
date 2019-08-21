using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.time
{
    class FuncChristmas : DefaultTimeable
    {
        private INumerable arg0;

        public FuncChristmas(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override DateTime ToTime()
        {
            int year = (int)arg0.ToNumber();
            TimeValidator.ValidateYear(year);

            return new DateTime(year, 12, 25, 0, 0, 0);
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return arg0.ToNumber();
                case TimeVariableType.Month:
                    return 12;
                case TimeVariableType.Day:
                    return 25;
                case TimeVariableType.WeekDay:
                    return DateExtractor.GetWeekDay(ToTime());
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
