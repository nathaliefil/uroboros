using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
