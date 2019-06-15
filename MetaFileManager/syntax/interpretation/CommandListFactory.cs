using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;

namespace DivineScript.syntax.commands
{
    class CommandListFactory
    {
        public static List<ICommand> Build(List<Token> tokens)
        {
            List<ICommand> commands = new List<ICommand>();

            for (int i = 0; i < tokens.Count; i++)
            {



            }



            return commands;
        }
    }
}
