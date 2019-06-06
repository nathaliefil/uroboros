using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    public class Token
    {
        TokenType type;
        string content;



        public Token(TokenType type)
        {
            this.type = type;
            this.content = "";
        }

        public Token(TokenType type, string content)
        {
            this.type = type;
            this.content = content;
        }
    }
}
