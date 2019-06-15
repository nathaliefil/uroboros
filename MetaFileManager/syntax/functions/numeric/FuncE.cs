using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncE : INumericFunction
    {

        public FuncE()
        {
        }

        public decimal ToNumber()
        {
            return (decimal) Math.E;
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            return value.ToString();
        }
    }
}
