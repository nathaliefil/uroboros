using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;
using DivineScript.syntax.reading;
using DivineScript.syntax.variables.refers;
using DivineScript.syntax.interpretation.vars_range;

namespace DivineScript.syntax.interpretation.expressions
{
    class BoolableBuilder
    {
        public static IBoolable Build(List<Token> tokens)
        {
            if (tokens.Count == 1 && tokens[0].GetTokenType().Equals(TokenType.Variable))
            {
                string str = tokens[0].GetContent();
                if (InterVariables.GetInstance().Contains(str, InterVarType.Bool))
                    return new BoolVariableRefer(str);
                else
                    return new NullVariable();
            }

            //code

            return new NullVariable();
        }
    }
}
