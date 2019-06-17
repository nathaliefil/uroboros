using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncPi : INumericFunction
    {

        public FuncPi()
        {
        }

        public override decimal ToNumber()
        {
            return (decimal)Math.PI;
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            return value.ToString();
        }
    }
}
