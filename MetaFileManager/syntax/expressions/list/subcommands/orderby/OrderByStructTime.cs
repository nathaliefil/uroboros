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

        public OrderByStructTime(OrderByVariable var, OrderByType typ, TimeVariableType tim) 
            : base(var, typ)
        {
            variable = var;
            type = typ;
            time = tim;
        }

        public TimeVariableType GetTimeVariable()
        {
            return time;
        }
    }
}
