using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class TimeableWithClock : DefaultTimeable
    {
        private ITimeable time;
        private IClock clock;

        public TimeableWithClock(ITimeable time, IClock clock)
        {
            this.time = time;
            this.clock = clock;
        }

        public override DateTime ToTime()
        {
            DateTime dtime = time.ToTime();

            int hours = (int)clock.ToHour();
            int minutes = (int)clock.ToMinute();
            int seconds = (int)clock.ToSecond();
            TimeValidator.ValidateClock(hours, minutes, seconds);

            TimeSpan ts = new TimeSpan(hours, minutes, seconds);
            dtime = dtime.Date + ts;

            return dtime;
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return time.ToTimeVariable(TimeVariableType.Year);
                case TimeVariableType.Month:
                    return time.ToTimeVariable(TimeVariableType.Month);
                case TimeVariableType.Day:
                    return time.ToTimeVariable(TimeVariableType.Day);
                case TimeVariableType.WeekDay:
                    return DateExtractor.GetWeekDay(ToTime());
                case TimeVariableType.Hour:
                    return clock.ToHour();
                case TimeVariableType.Minute:
                    return clock.ToMinute();
                case TimeVariableType.Second:
                    return clock.ToSecond();
            }
            return 0;
        }
    }
}
