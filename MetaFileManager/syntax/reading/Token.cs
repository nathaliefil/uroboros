using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    class Token
    {
        TokenType type;
        string content;



        Token(TokenType type)
        {
            this.type = type;
            this.content = "";
        }

        Token(TokenType type, string content)
        {
            this.type = type;
            this.content = content;
        }
    }
}
