using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.blocks
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
            RuntimeVariables.GetInstance().BracketsUp();
            foreach (ICommand command in commands)
            {
                command.Run();
            }
            RuntimeVariables.GetInstance().BracketsDown();
        }
    }
}
