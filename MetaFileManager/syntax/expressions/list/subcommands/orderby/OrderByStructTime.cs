using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.expressions.list.subcommands.orderby
{
    class OrderByStructTime : OrderByStruct
    {
        private TimeVariableType time;

        public OrderByStructTime(OrderByVariable var, TimeVariableType tim) 
            : base(var)
        {
            variable = var;
            time = tim;
            type = OrderByType.ASC;
        }

        public TimeVariableType GetTimeVariable()
        {
            return time;
        }
    }
}
