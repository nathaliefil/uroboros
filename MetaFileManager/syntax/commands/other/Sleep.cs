using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;
using System.Threading;

namespace Uroboros.syntax.commands.other
{
    class Sleep : ICommand
    {
        private INumerable time;

        public Sleep(INumerable time)
        {
            this.time = time;
        }

        public void Run()
        {
            int howlong = (int)(time.ToNumber() * 1000);
            if (howlong > 0)
                Thread.Sleep(howlong);
        }
    }
}
