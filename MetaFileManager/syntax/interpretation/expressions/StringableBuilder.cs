using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.expressions.strings;
using Uroboros.syntax.interpretation.functions;

namespace Uroboros.syntax.interpretation.expressions
{
    class StringableBuilder
    {
        public static IStringable Build(List<Token> tokens)
        {
            // try to build Numerable
            INumerable inu = NumerableBuilder.Build(tokens);
            if (!inu.IsNull())
                return (inu as IStringable);

            // try to build simple one-token Stringable
            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.String))
                        return new StringVariableRefer(str);
                    else
                        return null;
                }
                if (tokens[0].GetTokenType().Equals(TokenType.StringConstant))
                    return new StringConstant(tokens[0].GetContent());
            }

            //try to build string function
            if (tokens.Count > 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.BracketOn)
                && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                IStringable istr = InterStringFunction.Build(tokens);
                if (!istr.IsNull())
                    return istr;
            }

            // try to build reference to n-th element of list of strings
            if (tokens.Count > 3 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.SquareBracketOn)
                && tokens[tokens.Count-1].GetTokenType().Equals(TokenType.SquareBracketOff))
            {
                IStringable istr = InterListElement.Build(tokens);
                if (!istr.IsNull())
                    return istr;
            }

            // try to build concatenated string -> text merged by +
            if(tokens.Any(t => t.GetTokenType().Equals(TokenType.Plus)))
                return BuildConcatenated(tokens);
            else
                return null;
        }

        public static IStringable BuildConcatenated(List<Token> tokens)
        {
            List<Token> currentTokens = new List<Token>();
            List<IStringable> elements = new List<IStringable>();
            int level = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].GetTokenType().Equals(TokenType.BracketOn))
                    level++;
                if (tokens[i].GetTokenType().Equals(TokenType.BracketOff))
                    level--;

                if (tokens[i].GetTokenType().Equals(TokenType.Plus) && level == 0)
                {
                    if (currentTokens.Count > 0)
                    {
                        IStringable ist = StringableBuilder.Build(currentTokens);
                        if (ist.IsNull())
                            return null;
                        else
                            elements.Add(ist);
                        currentTokens.Clear();
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

            if (elements.Count > 0)
                return new StringExpression(elements);
            return null;
        }
    }
}
