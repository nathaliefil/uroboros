using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.blocks
{
    class RepeatBlock : Block, ICommand
    {

        private INumerable repeats;

        public RepeatBlock(List<ICommand> commands, INumerable repeats)
            : base(commands)
        {
            this.commands = commands;
            this.repeats = repeats;
        }

        new public void Run()
        {
            decimal i = 0;
            decimal times = repeats.ToNumber();

            RuntimeVariables.GetInstance().Actualize("index", 0);
            RuntimeVariables.GetInstance().BracketsUp();
            while (i < times)
            {
                foreach (ICommand command in commands)
                {
                    command.Run();
                }
                i++;
                RuntimeVariables.GetInstance().PlusPlus("index");
            }
            RuntimeVariables.GetInstance().BracketsDown();
            RuntimeVariables.GetInstance().Actualize("index", 0);
        }
    }
}
