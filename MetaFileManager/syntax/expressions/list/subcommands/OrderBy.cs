using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.expressions.list.subcommands
{
    public class OrderBy : ISubcommand
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
