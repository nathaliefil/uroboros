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
    class NumerableBuilder
    {
        public static INumerable Build(List<Token> tokens)
        {
            IBoolable ibo = BoolableBuilder.Build(tokens);
            if (!(ibo is NullVariable))
            {
                return ibo;
            }

            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.Number))
                        return new NumericVariableRefer(str);
                    else
                        return new NullVariable();
                }
                if (tokens[0].GetTokenType().Equals(TokenType.NumericConstant))
                {
                    return new NumericConstant(tokens[0].GetNumericContent());
                }
            }

            //code

            return new NullVariable();
        }
    }
}
