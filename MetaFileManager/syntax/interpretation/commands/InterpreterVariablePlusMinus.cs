using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.lexer;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.commands.arithmetic;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterVariablePlusMinus
    {
        public static ICommand Build(List<Token> tokens)
        {
            bool isPlusPlus = tokens[1].GetTokenType().Equals(TokenType.PlusPlus);
            string name = tokens[0].GetContent();
            string type = isPlusPlus ? "increment" : "decrement";

            if (tokens.Count > 2)
                throw new SyntaxErrorException("ERROR! Variable " + name + " " + type + "ation operation contains unnecessary code.");

            if (InterVariables.GetInstance().ContainsChangable(name, InterVarType.Number) &&
                !InterVariables.GetInstance().Contains(name, InterVarType.Bool))
            {
                if (isPlusPlus)
                    return new PlusPlus(name);
                else
                    return new MinusMinus(name);
            }
            throw new SyntaxErrorException("ERROR! Variable " + name + " cannot be " + type + "ed.");
        }
    }
}
