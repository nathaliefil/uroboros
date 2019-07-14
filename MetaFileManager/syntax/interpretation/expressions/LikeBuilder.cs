using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.reading;
using Uroboros.syntax.expressions.bools.other;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.interpretation.expressions
{
    class LikeBuilder
    {
        public static IBoolable Build(List<Token> tokens)
        {
            int index = tokens.TakeWhile(x => !x.GetTokenType().Equals(TokenType.Like)).Count();
            if (index == 0 || index == tokens.Count - 1)
                return null;

            List<Token> leftTokens = tokens.GetRange(0, index);
            List<Token> rightTokens = tokens.GetRange(index + 1, tokens.Count - index - 1);

            IStringable istr = StringableBuilder.Build(leftTokens);
            if (istr.IsNull())
                return null;

            if (rightTokens.Count == 1 && rightTokens[0].GetTokenType().Equals(TokenType.StringConstant))
            {
                string phrase = rightTokens[0].GetContent();
                CheckPhraseCorrectness(phrase);
                return new Like(istr, phrase);
            }
            else
                return null;
        }

        private static void CheckPhraseCorrectness(string phrase)
        {
            if (phrase.Length == 0)
                throw new SyntaxErrorException("ERROR! Expression 'like' has empty comparing phrase.");

            if (phrase.Length == 1)
                return;

            for (int i = 1; i < phrase.Length; i++)
            {
                if (phrase[i - 1] == '%' && (phrase[i] == '%' || phrase[i] == '_'))
                    throw new SyntaxErrorException("ERROR! Expression \"like " + phrase + "\" is not correct.");
            }
            return;
        }
    }
}
