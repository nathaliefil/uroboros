using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables.time
{
    abstract class DefaultTimeableWithClockConstant : DefaultTimeable
    {
        protected decimal hour;
        protected decimal minute;
        protected decimal second;
        protected decimal daysForward;


        // if values are out of range - cut them
        // for example: hour 21:72:11
        //    is changed to: 22:12:11
        protected void NormalizeClockVariables()
        {
            daysForward = 0;

            if (second > 60)
            {
                decimal rest = second % 60;
                minute += second / 60;
                second = rest;
            }
            if (second < 0)
            {
                decimal rest = second % 60;
                minute -= 1 + (-second) / 60;
                second = 60 + rest;
            }

            if (minute > 60)
            {
                decimal rest = minute % 60;
                hour += minute / 60;
                minute = rest;
            }
            if (minute < 0)
            {
                decimal rest = minute % 60;
                hour -= 1 + (-minute) / 60;
                minute = 60 + rest;
            }

            if (hour > 24)
            {
                decimal rest = hour % 24;
                daysForward += hour / 24;
                hour = rest;
            }
            if (hour < 0)
            {
                decimal rest = hour % 24;
                daysForward -= 1 + (-hour) / 24;
                hour = 24 + rest;
            }
        }
    }
}
