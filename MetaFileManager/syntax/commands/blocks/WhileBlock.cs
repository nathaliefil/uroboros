using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.blocks
{
    class WhileBlock : Block, ICommand
    {

        private IBoolable condition;

        public WhileBlock(List<ICommand> commands, IBoolable condition)
            : base(commands)
        {
            this.commands = commands;
            this.condition = condition;
        }

        new public void Run()
        {
            RuntimeVariables.GetInstance().BracketsUp();
            while (condition.ToBool())
            {
                foreach (ICommand command in commands)
                {
                    command.Run();
                }
            }
            RuntimeVariables.GetInstance().BracketsDown();
        }
    }
}
