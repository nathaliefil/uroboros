using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading.errors
{
    class TokenException: Exception
    {
        private string message;

        public TokenException(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
