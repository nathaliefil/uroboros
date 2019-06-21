using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.expressions.list.subcommands
{
    class OrderBy : ISubcommand
    {
        private List<OrderByStruct> variables;

        public OrderBy(List<OrderByStruct> variables)
        {
            this.variables = variables;
        }

        public List<OrderByStruct> GetVariables()
        {
            return variables;
        }
    }
}
