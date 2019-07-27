using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.expressions.list.subcommands.orderby
{
    class OrderByStructDate : OrderByStruct
    {
        public OrderByStructDate(OrderByVariable var, OrderByType typ)
            : base(var, typ)
        {
            variable = var;
            type = typ;
        }
    }
}
