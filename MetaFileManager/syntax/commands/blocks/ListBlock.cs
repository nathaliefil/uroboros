using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.expressions.list;

namespace DivineScript.syntax.commands.blocks
{
    class ListBlock : Block, ICommand
    {

        private ListExpression list;

        public ListBlock(List<ICommand> commands, ListExpression list)
            : base(commands)
        {
            this.commands = commands;
            this.list = list;
        }

        new public void Run()
        {
            RuntimeVariables.GetInstance().Actualize("index", 0);
            RuntimeVariables.GetInstance().BracketsUp();
            foreach(string element in list.ToList())
            {
                RuntimeVariables.GetInstance().Actualize("this", element);
                foreach (ICommand command in commands)
                {
                    command.Run();
                }
                RuntimeVariables.GetInstance().PlusPlus("index");
            }
            RuntimeVariables.GetInstance().BracketsDown();
            RuntimeVariables.GetInstance().Actualize("index", 0);
            RuntimeVariables.GetInstance().Actualize("this", "");
        }
    }
}
