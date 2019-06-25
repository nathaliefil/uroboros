using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.expressions.bools
{
    enum BoolExpressionOperatorType
    {
        Not,
        And,
        Or,
        Xor,

        BracketOn,
        BracketOff,

        Null
    }
}
