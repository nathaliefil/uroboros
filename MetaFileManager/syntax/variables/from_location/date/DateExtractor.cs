using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.from_location.date
{
    class DateExtractor
    {
        public static decimal GetVariableNumeric(DateVariableType type, DateTime time)
        {
            switch (type)
            {
                case DateVariableType.Year:
                    return time.Year;
                case DateVariableType.Month:
                    return time.Month;
                case DateVariableType.WeekDay:
                    return (decimal)time.DayOfWeek;
                case DateVariableType.Day:
                    return time.Day;
                case DateVariableType.Hour:
                    return time.Hour;
                case DateVariableType.Minute:
                    return time.Minute;
                case DateVariableType.Second:
                    return time.Second;
                case DateVariableType.Date:
                    return DateToInt(time);
                case DateVariableType.Clock:
                    return ClockToInt(time);
            }
            return 0;
        }

        public static string GetVariableString(DateVariableType type, DateTime time)
        {
            switch (type)
            {
                case DateVariableType.Time:
                    return Time(time);
                case DateVariableType.Date:
                    return Date(time);
                case DateVariableType.Clock:
                    return Clock(time);
            }
            return "";
        }

        private static string Time(DateTime time)
        {
            return Date(time) + ", " + Clock(time);
        }

        private static string Date(DateTime time)
        {
            return time.Day + Month(time.Month) + time.Year;
        }

        private static string Clock(DateTime time)
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

        private static string Month(int number)
        {
            switch (number)
            {
                case 1:
                    return " January ";
                case 2:
                    return " February ";
                case 3:
                    return " March ";
                case 4:
                    return " April ";
                case 5:
                    return " May ";
                case 6:
                    return " June ";
                case 7:
                    return " July ";
                case 8:
                    return " August ";
                case 9:
                    return " September ";
                case 10:
                    return " October ";
                case 11:
                    return " November ";
                case 12:
                    return " December ";
            }
            return "";
        }
    }
}
