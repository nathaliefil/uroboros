using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.commands.print
{
    class PrintList : ICommand
    {
        private IListable list;

        public PrintList(IListable list)
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
