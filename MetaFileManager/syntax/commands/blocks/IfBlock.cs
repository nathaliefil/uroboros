using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.blocks
{
    class IfBlock: Block, ICommand
    {

        private List<ICommand> elseCommands;
        private IBoolable condition;

        public IfBlock(List<ICommand> commands, IBoolable condition, List<ICommand> elseCommands) 
            : base (commands)
        {
            this.commands = commands;
            this.elseCommands = elseCommands;
            this.condition = condition;
        }

        public IfBlock(List<ICommand> commands, IBoolable condition) 
            : base(commands)
        {
            this.commands = commands;
            elseCommands = new List<ICommand>();
            this.condition = condition;
        }

        new public void Run()
        {
            if (condition.ToBool())
            {
                RuntimeVariables.GetInstance().BracketsUp();
                foreach (ICommand command in commands)
                {
                    command.Run();
                }
                RuntimeVariables.GetInstance().BracketsDown();
            }
            else
            {
                if (elseCommands.Count() > 0)
                {
                    RuntimeVariables.GetInstance().BracketsUp();
                    foreach (ICommand elseCommand in elseCommands)
                    {
                        elseCommand.Run();
                    }
                    RuntimeVariables.GetInstance().BracketsDown();
                }
            }
        }
    }
}
