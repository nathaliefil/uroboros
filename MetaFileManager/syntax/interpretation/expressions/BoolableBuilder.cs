using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.interpretation.vars_range;

namespace Uroboros.syntax.interpretation.expressions
{
    class BoolableBuilder
    {
        public static IBoolable Build(List<Token> tokens)
        {
            Token wwtok = TokenGroups.WrongTokenInExpression(tokens);
            if (!wwtok.GetTokenType().Equals(TokenType.Null))
                return new NullVariable();

            Brackets.CheckCorrectness(tokens, false);

            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.Bool))
                        return new BoolVariableRefer(str);
                    else
                        return new NullVariable();
                }
                if (tokens[0].GetTokenType().Equals(TokenType.BoolConstant))
                {
                    if (tokens[0].GetContent().Equals("true"))
                        return new BoolConstant(true);
                    else
                        return new BoolConstant(false);
                }
            }

            

            

            //code

            return new NullVariable();
        }
    }
}
