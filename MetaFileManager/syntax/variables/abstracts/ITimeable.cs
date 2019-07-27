using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.from_location.date;

namespace Uroboros.syntax.variables.abstracts
{
    interface ITimeable
    {
        DateTime ToTime();
        decimal ToTimeVariable(TimeVariableType type);
        string ToDate();
        string ToClock();
        string ToString();
    }
}
