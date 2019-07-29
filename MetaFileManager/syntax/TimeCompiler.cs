using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax
{
    class TimeCompiler
    {
        public static DateTime CreateDate(int year, int month, int day, int hour, int minute, int second)
        {
            int daysForward = 0;

            if (second > 60)
            {
                int rest = second % 60;
                minute += second / 60;
                second = rest;
            }
            if (second < 0)
            {
                int rest = second % 60;
                minute -= 1 + (-second) / 60;
                second = 60 + rest;
            }

            if (minute > 60)
            {
                int rest = minute % 60;
                hour += minute / 60;
                minute = rest;
            }
            if (minute < 0)
            {
                int rest = minute % 60;
                hour -= 1 + (-minute) / 60;
                minute = 60 + rest;
            }

            if (hour > 24)
            {
                int rest = hour % 24;
                daysForward += hour / 24;
                hour = rest;
            }
            if (hour < 0)
            {
                int rest = hour % 24;
                daysForward -= 1 + (-hour) / 24;
                hour = 24 + rest;
            }

            DateTime time = new DateTime(year, month, day, hour, minute, second);

            TimeValidator.ValidateYear(year + daysForward);

            if (daysForward != 0)
                time.AddDays(daysForward);

            return time;
        }
    }
}
