using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.time
{
    class RelativeTimeStruct
    {
        public RelativeTimeType type;
        public INumerable value;
        public TimeDirection timeDirection;

        public RelativeTimeStruct(RelativeTimeType type, INumerable value, TimeDirection timeDirection)
        {
            this.type = type;
            this.value = value;
            this.timeDirection = timeDirection;
        }
    }
}
