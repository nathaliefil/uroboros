using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.functions.time
{
    class FuncCreation : DefaultTimeable
    {
        private IStringable arg0;

        public FuncCreation(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override DateTime ToTime()
        {
            string file = arg0.ToString();
            return FileInnerVariable.GetCreation(file);
        }
    }
}
