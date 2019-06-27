using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
namespace Uroboros.syntax.functions.numeric
{
    class FuncPi : DefaultNumerable
    {
        public FuncPi()
        {
        }

        public override decimal ToNumber()
        {
            return 3.14159265358979M;
        }
    }
}
