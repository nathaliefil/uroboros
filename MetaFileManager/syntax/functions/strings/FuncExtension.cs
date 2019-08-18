using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.functions.strings
{
    class FuncExtension : DefaultStringable
    {
        private IStringable arg0;

        public FuncExtension(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            string file = arg0.ToString();
            return FileInnerVariable.GetExtension(file);
        }
    }
}
