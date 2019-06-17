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
    class ListableBuilder
    {
        public static IListable Build(List<Token> tokens)
        {
            IStringable ist = StringableBuilder.Build(tokens);
            if (!(ist is NullVariable))
            {
                return ist;
            }

            if (tokens.Count == 1 && tokens[0].GetTokenType().Equals(TokenType.Variable))
            {
                string str = tokens[0].GetContent();
                if (InterVariables.GetInstance().Contains(str, InterVarType.List))
                    return new ListVariableRefer(str);
                else
                    return new NullVariable();
            }
            

            //code

            return new NullVariable();
        }
    }
}
