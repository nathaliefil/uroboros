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

        public override decimal ToNumber()
        {
            return (decimal) Math.E;
        }
    }
}
