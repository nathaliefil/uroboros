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

        public OrderByStruct(OrderByVariable var)
        {
            variable = var;
            type = OrderByType.ASC;
        }

        public OrderByVariable GetVariable()
        {
            return variable;
        }

        public void SetDesc()
        {
            type = OrderByType.DESC;
        }

        public OrderByType GetOrderType()
        {
            return type;
        }
    }
}
