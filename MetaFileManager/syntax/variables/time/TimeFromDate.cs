﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    class TimeFromDate : DefaultTimeable
    {
        private INumerable day;
        private decimal month;
        private INumerable year;


        public TimeFromDate(INumerable day, decimal month, INumerable year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        public override DateTime ToTime()
        {
            decimal day2 = decimal.Truncate(day.ToNumber());
            decimal year2 = decimal.Truncate(year.ToNumber());
            TimeValidator.ValidateYear(year2);
            TimeValidator.ValidateDay(day2, month, year2);
            return new DateTime((int)year2, (int)month, (int)day2, 0, 0, 0);
        }


        public override decimal ToTimeVariable(TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return year.ToNumber();
                case TimeVariableType.Month:
                    return month;
                case TimeVariableType.Day:
                    return day.ToNumber();
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
