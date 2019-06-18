using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class ListVariable : NamedVariable, IListable
    {
        private List<string> values;

        public ListVariable(string name, List<string> values)
        {
            this.name = name;
            this.values = values;
        }

        public override List<string> ToList()
        {
            return values;
        }

        public void Add(string value)
        {
            values.Add(value);
        }

        public void Add(List<string> values)
        {
            values.AddRange(values);
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
    }
}
