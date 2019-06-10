using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.blocks
{
    class Block : ICommand
    {
        protected List<ICommand> commands;

        public Block(List<ICommand> commands)
        {
            this.commands = commands;
        }

        public void Run()
        {
            RuntimeVariables.BracketsUp();
            foreach (ICommand command in commands)
            {
                command.Run();
            }
            RuntimeVariables.BracketsDown();
        }
    }
}
