using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.print
{
    class PrintList : ICommand
    {
        private ListExpression list;

        public PrintList(ListExpression list)
        {
            this.list = list;
        }

        public void Run()
        {
            foreach (string element in list.ToList())
            {
                Logger.GetInstance().Log(element);
            }
        }
    }
}
