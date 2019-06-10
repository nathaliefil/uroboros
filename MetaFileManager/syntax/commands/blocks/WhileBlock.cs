using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.commands.blocks
{
    class WhileBlock : Block, ICommand
    {

        private BoolExpression condition;

        public WhileBlock(List<ICommand> commands, BoolExpression condition)
            : base(commands)
        {
            this.commands = commands;
            this.condition = condition;
        }

        new public void Run()
        {
            RuntimeVariables.BracketsUp();
            while (condition.ToBool())
            {
                foreach (ICommand command in commands)
                {
                    command.Run();
                }
            }
            RuntimeVariables.BracketsDown();
        }
    }
}
