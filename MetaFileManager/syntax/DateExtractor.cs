using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables;

namespace Uroboros.syntax
{
    class DateExtractor
    {
        public static decimal GetVariable(DateTime time, TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                    return time.Year;
                case TimeVariableType.Month:
                    return time.Month;
                case TimeVariableType.Day:
                    return time.Day;
                case TimeVariableType.WeekDay:
                    return (decimal)time.DayOfWeek;
                case TimeVariableType.Hour:
                    return time.Hour;
                case TimeVariableType.Minute:
                    return time.Minute;
                case TimeVariableType.Second:
                    return time.Second;
            }
            return 0;
        }

        public static string ToString(DateTime time)
        {
            return ToDate(time) + ", " + ToClock(time);
        }

        public static string ToDate(DateTime time)
        {
            return time.Day + " " + Month(time.Month) + " " + time.Year;
        }

        public static string ToClock(DateTime time)
        {
            return Filled(time.Hour) + ":" + Filled(time.Minute) + ":" + Filled(time.Second);
        }

        public static int ClockToInt(DateTime time)
        {
            return time.Hour * 3600 + time.Minute * 60 + time.Second;
        }

        public static int DateToInt(DateTime time)
        {
            return time.Year * 366 + time.Month * 31 + time.Day;
        }

        private static string Filled(int number)
        {
            if (number < 10)
                return "0" + number;
            else
                return number.ToString();
        }

        public static string Month(int number)
        {
            switch (number)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
            }
            return "";
        }
    }
}
