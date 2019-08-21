using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.time
{
    interface IClock
    {
        decimal ToHour();
        decimal ToMinute();
        decimal ToSecond();
    }
}
