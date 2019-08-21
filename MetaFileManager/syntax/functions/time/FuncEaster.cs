using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.time
{
    class FuncEaster : DefaultTimeable
    {
        private INumerable arg0;

        public FuncEaster(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override DateTime ToTime()
        {
            int year = (int)arg0.ToNumber();
            TimeValidator.ValidateYear(year);

            return Easter(year);
        }

        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return arg0.ToNumber();
                case TimeVariableType.Month:
                    return Easter((int)arg0.ToNumber()).Month;
                case TimeVariableType.Day:
                    return Easter((int)arg0.ToNumber()).Day;
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

        // nice stolen code
        // https://codereview.stackexchange.com/questions/193847/find-easter-on-any-given-year
        private DateTime Easter(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = (b - (b / 4) - ((8 * b + 13) / 25) + (19 * a) + 15) % 30;
            int d = c - (c / 28) * (1 - (c / 28) * (29 / (c + 1)) * ((21 - a) / 11));
            int e = d - ((year + (year / 4) + d + 2 - b + (b / 4)) % 7);
            int month = 3 + ((e + 40) / 44);
            int day = e + 28 - (31 * (month / 4));
            return new DateTime(year, month, day);
        }
    }
}
