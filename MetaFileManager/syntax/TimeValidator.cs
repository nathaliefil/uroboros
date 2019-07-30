using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax
{
    class TimeValidator
    {
        public static void ValidateDate(decimal day, decimal month, decimal year)
        {
            ValidateYear(year);
            ValidateMonth(month);
            ValidateDay(day, month, year);
        }

        public static void ValidateYear(decimal year)
        {
            if (!IsYearCorrect(year))
                throw new RuntimeException("RUNTIME ERROR! Year out of range occurred: " + year + ".");
        }

        public static void ValidateMonth(decimal month)
        {
            if (!IsMonthCorrect(month))
                throw new RuntimeException("RUNTIME ERROR! Non-existent month occurred: " + month + "th month.");
        }

        public static void ValidateDay(decimal day, decimal month, decimal year)
        {
            if (!IsDayCorrect(day, month, year))
                throw new RuntimeException("RUNTIME ERROR! Day out of month occured: " + day + "th day of " + DateExtractor.Month(month) + ".");
        }

        private static bool IsYearCorrect(decimal year)
        {
            if (year < 1 || year > 9999)
                return false;
            else
                return true;
        }

        private static bool IsMonthCorrect(decimal month)
        {
            if (month < 1 || month > 12)
                return false;
            else
                return true;
        }

        private static bool IsDayCorrect(decimal day, decimal month, decimal year)
        {
            if (day < 1)
                return false;

            decimal daysInMonth = 31;

            if (month == 4 || month == 6 || month == 9 || month == 1)
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
