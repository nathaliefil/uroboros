using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.commands.other;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterSelect
    {
        public static ICommand Build(List<Token> tokens)
        {
            tokens.RemoveAt(0);

            if (tokens.Count == 0)
                return new Select(new StringVariableRefer("this"));

            IListable ilist = ListableBuilder.Build(tokens);
            if (ilist.IsNull())
                throw new SyntaxErrorException("ERROR! There are is something wrong with elements declaration in select command.");
            else
                return new Select(ilist);
        }
    }
}
