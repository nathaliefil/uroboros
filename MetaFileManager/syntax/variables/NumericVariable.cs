using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables
{
    class NumericVariable : NamedVariable, INumerable, IStringable
    {
        private decimal value;

        public NumericVariable(string name, decimal value)
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

        public void PlusPlus()
        {
            value++;
        }

        public void MinusMinus()
        {
            value--;
        }

        public void IncreaseBy(decimal dec)
        {
            value += dec;
        }

        public void DecreaseBy(decimal dec)
        {
            value -= dec;
        }

        public void MultiplyBy(decimal dec)
        {
            value *= dec;
        }

        public void DivideBy(decimal dec)
        {
            if (dec == 0)
            {
                // throw some exception
                /// todo
            }
            value /= dec;
        }

    }
}
