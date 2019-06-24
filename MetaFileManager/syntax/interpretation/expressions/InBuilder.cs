using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.reading;
using Uroboros.syntax.expressions.bools.other;

namespace Uroboros.syntax.interpretation.expressions
{
    class InBuilder
    {
        public static IBoolable Build(List<Token> tokens)
        {
            int index = tokens.TakeWhile(x => !x.GetTokenType().Equals(TokenType.In)).Count();
            if (index == 0 || index == tokens.Count - 1)
                return new NullVariable();

            List<Token> leftTokens = tokens.GetRange(0, index);
            List<Token> rightTokens = tokens.GetRange(index + 1, tokens.Count - index - 1);

            IStringable istr = StringableBuilder.Build(leftTokens);
            if (istr is NullVariable)
                return new NullVariable();

            IListable ilis = ListedStringablesBuilder.Build(rightTokens);
            if (ilis is NullVariable)
                return new NullVariable();

            return new In(istr, ilis);
        }
    }
}
