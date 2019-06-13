using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.commands.blocks
{
    class RepeatBlock : Block, ICommand
    {

        private List<ICommand> elseCommands;
        private NumericExpression repeats;

        public RepeatBlock(List<ICommand> commands, NumericExpression repeats)
            : base(commands)
        {
            this.commands = commands;
            elseCommands = new List<ICommand>();
            this.repeats = repeats;
        }

        new public void Run()
        {
            decimal i = 0;
            decimal times = repeats.ToNumber();

            RuntimeVariables.GetInstance().BracketsUp();
            while (i < times)
            {
                foreach (ICommand command in commands)
                {
                    command.Run();
                }
                i++;
                //RuntimeVariables.GetInstance().Ac
            }
            RuntimeVariables.GetInstance().BracketsDown();
        }
    }
}
