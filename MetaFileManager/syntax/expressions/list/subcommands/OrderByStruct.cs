using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.expressions.list.subcommands
{
    public struct OrderByStruct
    {
        public OrderByVariable variable;
        public OrderByType type;

        public OrderByStruct(OrderByVariable var, OrderByType typ)
        {
            variable = var;
            type = typ;
        }
    }
}
