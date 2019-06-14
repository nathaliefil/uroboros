using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax
{
    class RuntimeException : Exception
    {
        private string message;

        public RuntimeException(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
