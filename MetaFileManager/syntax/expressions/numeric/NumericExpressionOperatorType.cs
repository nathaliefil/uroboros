using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.expressions.numeric
{
    enum NumericExpressionOperatorType
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Modulo,

        UnaryMinus,

        BracketOn,
        BracketOff,

        None
    }
}
