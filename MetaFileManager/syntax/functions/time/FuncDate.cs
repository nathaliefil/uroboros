using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.time
{
    class FuncDate : DefaultTimeable
    {
        private INumerable arg0;
        private INumerable arg1;
        private INumerable arg2;

        //                         DAY             MONTH             YEAR
        public FuncDate(INumerable arg0, INumerable arg1, INumerable arg2)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public override DateTime ToTime()
        {
            int day = (int)arg0.ToNumber();
            int month = (int)arg1.ToNumber();
            int year = (int)arg2.ToNumber();
            TimeValidator.ValidateDate(day, month, year);
            return new DateTime(year, month, day, 0, 0, 0);
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return arg2.ToNumber();
                case TimeVariableType.Month:
                    return arg1.ToNumber();
                case TimeVariableType.Day:
                    return arg0.ToNumber();
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
