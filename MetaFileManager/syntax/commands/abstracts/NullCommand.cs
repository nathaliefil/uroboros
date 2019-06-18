using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.commands.abstracts
{
    class NullCommand : ICommand
    {
        /*

        this command is never used runtime
        it's purpose is to inform interpreter
        that command interpretation went wrong
        will be deleted after testing phase
         
            */
        public NullCommand()
        {
        }

        public void Run()
        {
        }
    }
}
