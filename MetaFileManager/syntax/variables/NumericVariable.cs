using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables
{
    class NumericVariable : NamedVariable, INumerable
    {
        private decimal value;

        NumericVariable(string name, decimal value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            if (value % 1 == 0)
                return ((int)value).ToString();
            else
                return value.ToString();
        }

        public decimal ToNumber()
        {
            return value;
        }

        public void SetValue(decimal dec)
        {
            value = dec;
        }

    }
}
