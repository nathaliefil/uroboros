using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax
{
    class TimeValidator
    {
        public static void ValidateDate(int day, int month, int year)
        {
            ValidateYear(year);
            ValidateMonth(month);
            ValidateDay(day, month, year);
        }

        public static void ValidateClock(int hour, int minute, int second)
        {
            ValidateHour(hour);
            ValidateMinute(minute);
            ValidateSecond(second);
        }

        public static void ValidateYear(int year)
        {
            if (!IsYearCorrect(year))
                throw new RuntimeException("RUNTIME ERROR! Year out of range occurred: " + year + ".");
        }

        public static void ValidateMonth(int month)
        {
            if (!IsMonthCorrect(month))
                throw new RuntimeException("RUNTIME ERROR! Non-existent month occurred: " + month + "th month.");
        }

        public static void ValidateDay(int day, int month, int year)
        {
            if (!IsDayCorrect(day, month, year))
                throw new RuntimeException("RUNTIME ERROR! Day out of month occured: " + day + "th day of " + DateExtractor.Month(month) + ".");
        }

        public static void ValidateHour(int hour)
        {
            if (hour < 0 || hour > 24)
                throw new RuntimeException("RUNTIME ERROR! Hour out of range occurred: " + hour + ".");
        }

        public static void ValidateMinute(int minute)
        {
            if (minute < 0 || minute > 60)
                throw new RuntimeException("RUNTIME ERROR! Minute out of range occurred: " + minute + ".");
        }

        public static void ValidateSecond(int second)
        {
            if (second < 0 || second > 60)
                throw new RuntimeException("RUNTIME ERROR! Second out of range occurred: " + second + ".");
        }

        private static bool IsYearCorrect(int year)
        {
            if (year < 1 || year > 9999)
                return false;
            else
                return true;
        }

        private static bool IsMonthCorrect(int month)
        {
            if (month < 1 || month > 12)
                return false;
            else
                return true;
        }

        private static bool IsDayCorrect(int day, int month, int year)
        {
            if (day < 1)
                return false;

            int daysInMonth = 31;

            if (month == 4 || month == 6 || month == 9 || month == 11)
                daysInMonth = 30;

            if (month == 2)
            {
                if (DateTime.IsLeapYear((int)year))
                    daysInMonth = 29;
                else
                    daysInMonth = 28;
            }

            if (day > daysInMonth)
                return false;
            else
                return true;
        }
    }
}
