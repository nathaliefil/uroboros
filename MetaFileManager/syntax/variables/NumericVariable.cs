﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class NumericVariable : NamedNumerable
    {
        private decimal value;

        public NumericVariable(string name, decimal value)
        {
            this.name = name;
            this.value = value;
        }

        public override decimal ToNumber()
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

        public void IncrementBy(decimal dec)
        {
            value += dec;
        }

        public void DecrementBy(decimal dec)
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
                throw new RuntimeException("RUNTIME ERROR! Division by zero occured. Variable: " + name + ".");
            value /= dec;
        }

        public void ModuloBy(decimal dec)
        {
            if (dec == 0)
                throw new RuntimeException("RUNTIME ERROR! Modulo by zero occured. Variable: " + name + ".");
            value %= dec;
        }
    }
}
