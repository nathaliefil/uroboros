using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.commands.structures
{
    abstract class BracketOn : ICommand
    {
        protected int commandNumber;

        public void Run()
        {
            // bracket do not perform actions
        }

        public int GetCommandNumber()
        {
            return commandNumber;
        }
    }
}
