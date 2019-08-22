using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.lexer;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.commands.other;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterSleep
    {
        public static ICommand Build(List<Token> tokens)
        {
            tokens.RemoveAt(0);
            if (tokens.Count == 0)
                return new Sleep(new NumericConstant(1));
            INumerable inum = NumerableBuilder.Build(tokens);
            if (inum.IsNull())
                throw new SyntaxErrorException("ERROR! Sleeping time cannot be read as number.");
            else
                return new Sleep(inum);
        }
    }
}
