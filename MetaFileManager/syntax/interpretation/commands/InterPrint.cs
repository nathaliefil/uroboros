using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.commands;
using DivineScript.syntax.reading;
using DivineScript.syntax.interpretation.expressions;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.commands.print;
using DivineScript.syntax.variables.refers;

namespace DivineScript.syntax.interpretation.commands
{
    class InterPrint
    {
        public static ICommand Build(List<Token> tokens)
        {
            tokens.RemoveAt(0);
            if (tokens.Count == 0)
            {
                return new Print(new StringVariableRefer("this"));
            }
            IListable ilist = ListableBuilder.Build(tokens);
            if (ilist is NullVariable)
                throw new SyntaxErrorException("ERROR! There are is something wrong with print command.");
            else
                return new Print(ilist);
        }
    }
}
