using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.time
{
    class FuncYesterday__2args : DefaultTimeable
    {
        private INumerable arg0;
        private INumerable arg1;

        //                                HOURS            MINUTES
        public FuncYesterday__2args(INumerable arg0, INumerable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override DateTime ToTime()
        {
            int hour = (int)arg0.ToNumber();
            int minute = (int)arg1.ToNumber();

            TimeValidator.ValidateHour(hour);
            TimeValidator.ValidateMinute(minute);

            DateTime s = DateTime.Now.AddDays(-1);
            TimeSpan ts = new TimeSpan(hour, minute, 0);
            s = s.Date + ts;
            return s;
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return DateTime.Now.AddDays(-1).Year;
                case TimeVariableType.Month:
                    return DateTime.Now.AddDays(-1).Month;
                case TimeVariableType.Day:
                    return DateTime.Now.AddDays(-1).Day;
                case TimeVariableType.WeekDay:
                    return DateExtractor.GetWeekDay(ToTime());
                case TimeVariableType.Hour:
                    return (int)arg0.ToNumber();
                case TimeVariableType.Minute:
                    return (int)arg1.ToNumber();
                case TimeVariableType.Second:
                    return 0;
            }
            return 0;
        }
    }
}
