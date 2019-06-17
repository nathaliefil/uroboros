﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables
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