using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.functions.numeric
{
    class FuncTimeBetween : DefaultNumerable
    {
        private ITimeable arg0;
        private ITimeable arg1;
        private TimeVariableType varType;

        public FuncTimeBetween(ITimeable arg0, ITimeable arg1, TimeVariableType varType)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.varType = varType;
        }

        public override decimal ToNumber()
        {
            switch (varType)
            {
                case TimeVariableType.Year:
                    return YearsBetween();
                case TimeVariableType.Month:
                    return MonthsBetween();
                case TimeVariableType.Day:
                    return DaysBetween();
                case TimeVariableType.Hour:
                    return HoursBetween();
                case TimeVariableType.Minute:
                    return MinutesBetween();
                case TimeVariableType.Second:
                    return SecondsBetween();
            }
            return 0;
        }

        private decimal YearsBetween()
        {
            return Math.Abs((arg0.ToTime().Year - arg1.ToTime().Year));
        }

        private decimal MonthsBetween()
        {
            DateTime date0 = arg0.ToTime();
            DateTime date1 = arg1.ToTime();
            return Math.Abs(((date0.Year - date1.Year) * 12) + date0.Month - date1.Month);
        }

        private decimal DaysBetween()
        {
            return Math.Abs((arg0.ToTime() - arg1.ToTime()).Days);
        }

        private decimal HoursBetween()
        {
            return Math.Abs(Math.Floor((decimal)(arg0.ToTime() - arg1.ToTime()).TotalHours));
        }

        private decimal MinutesBetween()
        {
            return Math.Abs(Math.Floor((decimal)(arg0.ToTime() - arg1.ToTime()).TotalMinutes));
        }

        private decimal SecondsBetween()
        {
            return Math.Abs(Math.Floor((decimal)(arg0.ToTime() - arg1.ToTime()).TotalSeconds));
        }

    }
}
