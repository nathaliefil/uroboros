using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.other
{
    class ClearLog : ICommand
    {
        public ClearLog()
        {
        }

        public void Run()
        {
            Logger.GetInstance().ClearLog();
        }
    }
}
