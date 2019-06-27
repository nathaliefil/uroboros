using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.variables.constants;

namespace Uroboros.syntax.interpretation.expressions
{
    class ListedStringablesBuilder
    {
        public static IListable Build(List<Token> tokens)
        {
            List<Token> currentTokens = new List<Token>();
            List<IStringable> elements = new List<IStringable>();
            int level = 0;

            if (tokens[0].GetTokenType().Equals(TokenType.BracketOn) && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();
                tokensCopy.RemoveAt(tokens.Count - 1);
                tokensCopy.RemoveAt(0);
                tokens = tokensCopy;
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].GetTokenType().Equals(TokenType.BracketOn))
                    level++;
                if (tokens[i].GetTokenType().Equals(TokenType.BracketOff))
                    level--;

                if (tokens[i].GetTokenType().Equals(TokenType.Comma) && level == 0)
                {
                    if (currentTokens.Count > 0)
                    {
                        IStringable ist = StringableBuilder.Build(currentTokens);
                        currentTokens.Clear();
                        if (ist.IsNull())
                            return null;
                        else
                            elements.Add(ist);
                    }
                }
                else
                    currentTokens.Add(tokens[i]);
            }

            if (currentTokens.Count > 0)
            {
                IStringable ist = StringableBuilder.Build(currentTokens);
                if (ist.IsNull())
                    return null;
                else
                    elements.Add(ist);
            }

            if (elements.Count == 0)
                return null;

            if (elements.All(e => e is StringConstant))
                return new ListConstant(elements.Select(e => e.ToString()).ToList());
            else
                return new ListedStringables(elements);
        }
    }
}
