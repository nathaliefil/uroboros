using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.expressions.list.subcommands.orderby
{
    class OrderByStructClock : OrderByStruct
    {
        public OrderByStructClock(OrderByVariable var)
            : base(var)
        {
            variable = var;
            type = OrderByType.ASC;
        }
    }
}
