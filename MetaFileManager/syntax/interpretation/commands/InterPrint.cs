using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.commands.print;
using Uroboros.syntax.variables.refers;

namespace Uroboros.syntax.interpretation.commands
{
    class InterPrint
    {
        public static ICommand Build(List<Token> tokens)
        {
            tokens.RemoveAt(0);
            if (tokens.Count == 0)
                return new Print(new StringVariableRefer("this"));
            IListable ilist = ListableBuilder.Build(tokens);
            if (ilist is NullVariable)
                throw new SyntaxErrorException("ERROR! There are is something wrong with print command.");
            else
                return new Print(ilist);
        }
    }
}
