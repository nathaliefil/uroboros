using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.expressions.list.subcommands.orderby
{
    public class OrderByStruct
    {
        protected OrderByVariable variable;
        protected OrderByType type;

        public OrderByStruct(OrderByVariable var, OrderByType typ)
        {
            variable = var;
            type = typ;
        }

        public OrderByVariable GetVariable()
        {
            return variable;
        }

        public OrderByType GetOrderType()
        {
            return type;
        }
    }
}
