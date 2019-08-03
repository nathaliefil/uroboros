using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.commands
{
    class CommandException : Exception
    {
        private string message;

        public CommandException(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
