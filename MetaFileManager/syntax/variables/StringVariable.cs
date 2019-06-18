using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class StringVariable :  NamedVariable, IStringable
    {
        private string value;

        public StringVariable(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

        public void SetValue(string str)
        {
            value = String.Copy(str);
        }

    }
}
