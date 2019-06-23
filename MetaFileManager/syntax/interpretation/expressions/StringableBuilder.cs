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

namespace Uroboros.syntax.interpretation.expressions
{
    class StringableBuilder
    {
        public static IStringable Build(List<Token> tokens)
        {
            INumerable inu = NumerableBuilder.Build(tokens);
            if (!(inu is NullVariable))
            {
                return inu;
            }

            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.String))
                        return new StringVariableRefer(str);
                    else
                        return new NullVariable();
                }
                if (tokens[0].GetTokenType().Equals(TokenType.StringConstant))
                {
                    return new StringConstant(tokens[0].GetContent());
                }
            }
            if(tokens.Any(t => t.GetTokenType().Equals(TokenType.Plus)))
                return BuildConcatenated(tokens);
            else
                return new NullVariable();
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
                        if (ist is NullVariable)
                            return new NullVariable();
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
                if (ist is NullVariable)
                    return new NullVariable();
                else
                    elements.Add(ist);
            }

            if (elements.Count > 0)
                return new StringExpression(elements);
            return new NullVariable();
        }
    }
}
