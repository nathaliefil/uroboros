﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.time
{
    class RelativeTimeExpression : DefaultTimeable
    {
        private List<RelativeTimeStruct> variables;
        private ITimeable referenceTime;

        public RelativeTimeExpression(List<RelativeTimeStruct> variables, ITimeable referenceTime)
        {
            this.variables = variables;
            this.referenceTime = referenceTime;
        }

        public override DateTime ToTime()
        {
            DateTime source = referenceTime.ToTime();

            foreach (RelativeTimeStruct var in variables)
            {
                int count = (int)var.value.ToNumber();

                if (var.timedirection == TimeDirection.Before)
                    count *= -1;

                switch (var.type)
                {
                    case RelativeTimeType.Years:
                        source = source.AddYears(count);
                        break;
                    case RelativeTimeType.Months:
                        source = source.AddMonths(count);
                        break;
                    case RelativeTimeType.Weeks:
                        source = source.AddDays(count * 7);
                        break;
                    case RelativeTimeType.Days:
                        source = source.AddDays(count);
                        break;
                    case RelativeTimeType.Hours:
                        source = source.AddHours(count);
                        break;
                    case RelativeTimeType.Minutes:
                        source = source.AddMinutes(count);
                        break;
                    case RelativeTimeType.Seconds:
                        source = source.AddYears(count);
                        break;
                }
            }
            return source;
        }
    }
}