using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric.abstracts
{
    abstract class INumericFunction : DefaultToListMethod, INumerable
    {
        public virtual decimal ToNumber()
        {
            return 0;
        }
    }
}
