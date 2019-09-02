using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.log;

namespace Uroboros.syntax.commands.other
{
    class Print : ICommand
    {
        private IListable list;

        public Print(IListable list)
        {
            this.list = list;
        }

        public void Run()
        {
            if (list is IStringable)
                Logger.GetInstance().Log((list as IStringable).ToString());
            else
            {
                foreach (string element in list.ToList())
                    Logger.GetInstance().Log(element);
            }
        }
    }
}
