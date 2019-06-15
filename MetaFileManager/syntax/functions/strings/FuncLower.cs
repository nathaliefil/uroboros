using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.functions.numeric
{
    class FuncLower : IStringFunction
    {
        private StringExpression arg0;

        public FuncLower(StringExpression arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            return (arg0.ToString()).ToLower() ;
        }
    }
}
