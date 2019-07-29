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

            if (month == 4 || month == 6 || month == 9 || month == 1)
                daysInMonth = 30;

            if (month == 2)
            {
                if (DateTime.IsLeapYear(year))
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
