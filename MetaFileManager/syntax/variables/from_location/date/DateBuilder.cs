﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.from_location.date
{
    class DateBuilder
    {
        public static string Build(DateTime time)
        {
            return time.Day + Month(time.Month) + time.Year + ", "+
                Filled(time.Hour) + ":" + Filled(time.Minute) + ":" + Filled(time.Second);
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
