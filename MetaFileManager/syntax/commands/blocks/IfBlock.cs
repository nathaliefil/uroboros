using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.commands.blocks
{
    class IfBlock: Block, ICommand
    {

        private List<ICommand> elseCommands;
        private BoolExpression condition;

        public IfBlock (List<ICommand> commands, BoolExpression condition ,List<ICommand> elseCommands) 
            : base (commands)
        {
            this.commands = commands;
            this.elseCommands = elseCommands;
            this.condition = condition;
        }

        public IfBlock(List<ICommand> commands, BoolExpression condition) 
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
