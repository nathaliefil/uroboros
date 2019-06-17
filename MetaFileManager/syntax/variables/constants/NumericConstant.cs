﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables
{
    class NumericConstant : INumerable
    {
        private decimal value;

        public NumericConstant(decimal value)
        {
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

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}