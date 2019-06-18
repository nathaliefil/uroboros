using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.tokenlists;

namespace Uroboros.syntax.commands
{
    class CommandListFactory
    {
        public static List<ICommand> Build(List<Token> tokens)
        {
            BlockTokenList mainBlock = new BlockTokenList(tokens);
            return mainBlock.ToCommands();
        }
    }
}
