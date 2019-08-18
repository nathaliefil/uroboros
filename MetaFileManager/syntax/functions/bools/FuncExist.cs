using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.functions.bools
{
    class FuncExist : DefaultBoolable
    {
        private IStringable arg0;

        public FuncExist(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override bool ToBool()
        {
            string file = arg0.ToString();
            return FileInnerVariable.Exist(file);
        }
    }
}
