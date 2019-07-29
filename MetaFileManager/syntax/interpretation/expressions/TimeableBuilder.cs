using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.refers;

namespace Uroboros.syntax.interpretation.expressions
{
    class TimeableBuilder
    {
        public static ITimeable Build(List<Token> tokens)
        {
            // try to build simple one-token Timeable
            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.Time))
                        return new TimeVariableRefer(str);
                }
            }

            // try to build relative time expression
            if (tokens.Where(t => t.GetTokenType().Equals(TokenType.After) 
                || t.GetTokenType().Equals(TokenType.Before)).Count() > 0)
                return BuildRelativeTime(tokens);

            return null;
        }

        public static ITimeable BuildRelativeTime(List<Token> tokens)
        {
            // write here
            /// todo

            throw new SyntaxErrorException("ERROR! Test test test test");
        }
    }
}
