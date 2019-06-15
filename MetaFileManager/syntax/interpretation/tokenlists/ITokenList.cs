using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.commands;

namespace DivineScript.syntax.interpretation.tokenlists
{
    interface ITokenList
    {
        List<ICommand> ToCommands();
    }
}
