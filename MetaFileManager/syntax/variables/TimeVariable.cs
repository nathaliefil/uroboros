using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class TimeVariable : NamedTimeable
    {
        private DateTime value;

        public TimeVariable(string name, DateTime value)
        {
            this.name = name;
            this.value = value;
        }

        public override DateTime ToTime()
        {
            return value;
        }

        public void SetValue(DateTime dec)
        {
            value = dec;
        }

        public void SetElementValue(decimal newValue, TimeVariableType type)
        {
            switch (type)
            {
                case TimeVariableType.Year:
                {
                    TimeValidator.ValidateYear((int)newValue);
                    ChangeYear((int)newValue); // special method considering leap years
                    break;
                }
                case TimeVariableType.Month:
                {
                    TimeValidator.ValidateMonth((int)newValue);
                    TimeValidator.ValidateDay(value.Day, (int)newValue, value.Day);
                    value = new DateTime(value.Year, (int)newValue, value.Day);
                    break;
                }
                case TimeVariableType.Day:
                {
                    TimeValidator.ValidateDay(value.Day, value.Month, (int)newValue);
                    value = new DateTime(value.Year, value.Month, (int)newValue);
                    break;
                }
                case TimeVariableType.Hour:
                {
                    TimeValidator.ValidateHour((int)newValue);
                    TimeSpan ts = new TimeSpan((int)newValue, value.Minute, value.Second);
                    value = value.Date + ts;
                    break;
                }
                case TimeVariableType.Minute:
                {
                    TimeValidator.ValidateMinute((int)newValue);
                    TimeSpan ts = new TimeSpan(value.Hour, (int)newValue, value.Second);
                    value = value.Date + ts;
                    break;
                }
                case TimeVariableType.Second:
                {
                    TimeValidator.ValidateSecond((int)newValue);
                    TimeSpan ts = new TimeSpan(value.Hour, value.Minute, (int)newValue);
                    value = value.Date + ts;
                    break;
                }
            }
        }

        private void ChangeYear(int newYear)
        {
            value = value.AddYears(newYear - value.Year);
        }
    }
}
