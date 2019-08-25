using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.runtime
{
    public partial class RuntimeVariables
    {
        public void Success()
        {
            Named nv = variables.First(v => v.GetName().Equals("success"));
            (nv as Success).Succeed();
        }

        public void Failure()
        {
            Named nv = variables.First(v => v.GetName().Equals("success"));
            (nv as Success).Failed();
        }
    }
}
