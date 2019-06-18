﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.variables
{
    class BoolVariable : NamedVariable, IBoolable
    {
        private bool value;

        public BoolVariable(string name, bool value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            if (value)
                return "1";
            else
                return "0";
        }

        public bool ToBool()
        {
            return value;
        }

        public decimal ToNumber()
        {
            if (value)
                return 1;
            else
                return 0;
        }

        public void SetValue(bool b)
        {
            value = b;
        }
    }
}
