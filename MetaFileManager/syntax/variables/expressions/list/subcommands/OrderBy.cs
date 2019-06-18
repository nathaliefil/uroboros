using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.expressions.list.subcommands
{
    class OrderBy : ISubcommand
    {
        private string variable;
        private OrderByType type;

        public OrderBy(string variable, OrderByType type)
        {
            this.variable = variable;
            this.type = type;
        }

        public string GetVariable()
        {
            return variable;
        }

        public OrderByType GetOrderType()
        {
            return type;
        }
    }
}
