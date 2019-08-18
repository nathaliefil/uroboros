using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.functions.numeric
{
    class FuncSize : DefaultNumerable
    {
        private IStringable arg0;

        public FuncSize(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            string file = arg0.ToString();
            return FileInnerVariable.GetSize(file);
        }
    }
}
