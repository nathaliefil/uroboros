using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.interpretation.functions;

namespace Uroboros.syntax.interpretation.expressions
{
    class NumerableBuilder
    {
        public static INumerable Build(List<Token> tokens)
        {
            IBoolable ibo = BoolableBuilder.Build(tokens);
            if (!(ibo is NullVariable))
                return ibo;

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
                    return new NumericConstant(tokens[0].GetNumericContent());
            }

            if (tokens.Count > 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.BracketOn)
                && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                INumerable inu = InterNumericFunction.Build(tokens);
                if (!(inu is NullVariable))
                    return inu;
            }

            //code

            return new NullVariable();
        }
    }
}
