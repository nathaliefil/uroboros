using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.strings
{
    class FuncLetters : DefaultStringable
    {
        private IStringable arg0;

        public FuncLetters(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            string value = arg0.ToString();

            if (string.IsNullOrEmpty(value)) 
                return value;
            else
                return new string(value.Where(char.IsLetter).ToArray());
        }
    }
}
