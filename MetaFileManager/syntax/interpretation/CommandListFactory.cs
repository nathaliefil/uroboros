using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.interpretation.tokenlists;

namespace DivineScript.syntax.commands
{
    class CommandListFactory
    {
        public static List<ICommand> Build(List<Token> tokens)
        {
            List<ICommand> commands = new List<ICommand>();

            BlockTokenList mainBlock = new BlockTokenList(tokens);



            return commands;
        }
    }
}
