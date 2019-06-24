using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.functions.numeric.abstracts;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_location;

namespace Uroboros.syntax.functions.bools
{
    class FuncExist : IBoolFunction
    {
        private IStringable arg0;

        public FuncExist(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override bool ToBool()
        {
            return FileInnerVariable.Exist(arg0.ToString());
        }
    }
}
