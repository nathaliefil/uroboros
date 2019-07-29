using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.time
{
    struct RelativeTimeStruct
    {
        public RelativeTimeType type;
        public INumerable value;
        public TimeDirection timedirection;

        public RelativeTimeStruct(RelativeTimeType type, INumerable value, TimeDirection timedirection)
        {
            this.type = type;
            this.value = value;
            this.timedirection = timedirection;
        }
    }
}
