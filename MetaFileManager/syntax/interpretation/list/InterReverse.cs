using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.commands;
using DivineScript.syntax.interpretation.vars_range;
using DivineScript.syntax.commands.list;

namespace DivineScript.syntax.interpretation.list
{
    class InterReverse
    {
        public static ICommand Build(List<Token> tokens)
        {
            if (tokens.Count == 1)
                throw new SyntaxErrorException("ERROR! Reverse command do not contain variable name.");
            if (tokens.Count > 2)
                throw new SyntaxErrorException("ERROR! Reverse command is too long.");
            if (!tokens[1].GetTokenType().Equals(TokenType.Variable))
                throw new SyntaxErrorException("ERROR! Variable name of reverse command cannot be read.");

            string str = tokens[1].GetContent();

            if (!InterVariables.GetInstance().Contains(str, InterVarType.String) && InterVariables.GetInstance().Contains(str, InterVarType.List))
                return new Reverse(str);
            else
                throw new SyntaxErrorException("ERROR! In reverse command variable " + str + " do not exist, cannnot be read as list or cannot be modified.");
        }
    }
}
