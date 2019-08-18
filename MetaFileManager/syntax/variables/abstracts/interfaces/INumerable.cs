using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.numeric;

namespace Uroboros.syntax.variables.abstracts
{
    interface INumerable : INumericExpressionElement
    {
        decimal ToNumber();
    }
}
