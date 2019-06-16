using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncLower : IStringFunction
    {
        private IStringable arg0;

        public FuncLower(IStringable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return (arg0.ToString()).ToLower() ;
        }
    }
}
