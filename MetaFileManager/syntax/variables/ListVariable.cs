using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables
{
    class ListVariable : NamedVariable, IListable
    {
        private List<string> values;

        ListVariable(List<string> values)
        {
            this.values = values;
        }

        ListVariable(string value)
        {
            this.values = new List<string>();
            values.Add(value);
        }

        public List<string> ToList()
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

    }
}
