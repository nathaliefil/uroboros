using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    class TokenGroups
    {
        private static TokenType[] ALLOWED_SUBCOMMAND = new TokenType[] { TokenType.Where, TokenType.First, 
            TokenType.Last, TokenType.Skip,  TokenType.Each, TokenType.OrderBy};


        public static bool IsSubcommandKeyword(TokenType type)
        {
            return ALLOWED_SUBCOMMAND.Contains(type) ? true : false;
        }

    }
}
