﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.lexer;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.commands.other;
using Uroboros.syntax.variables.refers;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterPrint
    {
        public static ICommand Build(List<Token> tokens)
        {
            if (tokens.Count == 0)
                return new Print(new StringVariableRefer("this"));
            IListable ilist = ListableBuilder.Build(tokens);
            if (ilist.IsNull())
                throw new SyntaxErrorException("ERROR! There are is something wrong with print command.");
            else
                return new Print(ilist);
        }
    }
}
