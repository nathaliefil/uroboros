using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.bools;

namespace Uroboros.syntax.variables.abstracts
{
    interface IBoolable : IBoolExpressionElement
    {
        bool ToBool();
    }
}
