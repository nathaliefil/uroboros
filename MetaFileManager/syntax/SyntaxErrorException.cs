using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax
{
    class SyntaxErrorException : Exception
    {
        private string message;

        public SyntaxErrorException(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
