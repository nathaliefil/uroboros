using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.abstracts
{
    abstract class DefaultToListMethod
    {
        public virtual List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
