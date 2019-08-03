using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.functions.bools
{
    class FuncExistinside : DefaultBoolable
    {
        private IStringable arg0;
        private IStringable arg1;

        public FuncExistinside(IStringable arg0, IStringable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override bool ToBool()
        {
            string file = arg0.ToString();
            string directory = arg1.ToString();

            if (file.Equals("") || directory.Equals(""))
                return false;

            return FileInnerVariable.ExistInside(file, directory);
        }
    }
}
