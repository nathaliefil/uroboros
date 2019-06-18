using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class StringConstant : IStringable
    {
        private string value;

        public StringConstant(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
