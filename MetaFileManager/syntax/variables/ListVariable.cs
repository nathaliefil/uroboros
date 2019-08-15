using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.expressions.list.subcommands.orderby;

namespace Uroboros.syntax.variables
{
    class ListVariable : NamedListable
    {
        private List<string> values;

        public ListVariable(string name, List<string> values)
        {
            this.name = name;
            this.values = values;
        }

        public override List<string> ToList()
        {
            return new List<string>(values.ToArray()); // make deep copy
        }

        public void Add(string value)
        {
            values.Add(value);
        }

        public void Add(List<string> valuess)
        {
            values.AddRange(valuess);
        }

        public void Reverse()
        {
            values.Reverse();
        }

        public void Remove(string value)
        {
            values.RemoveAll(v => v.Equals(value));
        }

        public void Remove(List<string> valuess)
        {
            values.RemoveAll(v => valuess.Contains(v));
        }

        public void SetValue(List<string> valuess)
        {
            values = valuess;
        }

        public void Order(OrderBy order)
        {
            values = OrderByExecutor.OrderBy(values, order);
        }

        public void SetElementAtIndex(string value, int index)
        {
            if (index < 0 || index >= values.Count)
                throw new RuntimeException("RUNTIME ERROR! Index out of list " + name + " occured: index " + index + ".");

            values[index] = value;
        }
    }
}
