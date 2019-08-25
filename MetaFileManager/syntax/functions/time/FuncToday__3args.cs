using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.time
{
    class FuncToday__3args : DefaultTimeable
    {
        private INumerable arg0;
        private INumerable arg1;
        private INumerable arg2;

        //                                HOURS            MINUTES         SECONDS
        public FuncToday__3args(INumerable arg0, INumerable arg1, INumerable arg2)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public override DateTime ToTime()
        {
            int hour = (int)arg0.ToNumber();
            int minute = (int)arg1.ToNumber();
            int second = (int)arg2.ToNumber();

            TimeValidator.ValidateHour(hour);
            TimeValidator.ValidateMinute(minute);
            TimeValidator.ValidateSecond(second);

            DateTime s = DateTime.Now;
            TimeSpan ts = new TimeSpan(hour, minute, second);
            s = s.Date + ts;
            return s;
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return DateTime.Now.Year;
                case TimeVariableType.Month:
                    return DateTime.Now.Month;
                case TimeVariableType.Day:
                    return DateTime.Now.Day;
                case TimeVariableType.WeekDay:
                    return DateExtractor.GetWeekDay(ToTime());
                case TimeVariableType.Hour:
                    return (int)arg0.ToNumber();
                case TimeVariableType.Minute:
                    return (int)arg1.ToNumber();
                case TimeVariableType.Second:
                    return (int)arg2.ToNumber();
            }
            return 0;
        }
    }
}
