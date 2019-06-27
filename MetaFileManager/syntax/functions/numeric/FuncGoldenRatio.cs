using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncGoldenRatio : DefaultNumerable
    {
        public FuncGoldenRatio()
        {
        }

        public override decimal ToNumber()
        {
            return 1.61803398874989M;
        }
    }
}
