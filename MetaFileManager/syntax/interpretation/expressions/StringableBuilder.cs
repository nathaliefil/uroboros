using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;
using DivineScript.syntax.reading;
using DivineScript.syntax.interpretation.vars_range;
using DivineScript.syntax.variables.refers;

namespace DivineScript.syntax.interpretation.expressions
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

            //code

            return new NullVariable();
        }
    }
}
