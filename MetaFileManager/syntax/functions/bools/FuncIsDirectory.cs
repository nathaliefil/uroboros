using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.functions.bools
{
    class FuncIsDirectory : DefaultBoolable
    {
        private IStringable arg0;

        public FuncIsDirectory(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override bool ToBool()
        {
            string file = arg0.ToString();
            return FileValidator.IsDirectory(file);
        }
    }
}
