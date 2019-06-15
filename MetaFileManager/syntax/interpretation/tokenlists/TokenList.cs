using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.commands;

namespace DivineScript.syntax.interpretation.tokenlists
{
    class TokenList : ITokenList
    {
        private List<Token> tokens;

        public TokenList(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public List<ICommand> ToCommands()
        {
            List<ICommand> commands = new List<ICommand>();

            // code

            return commands;
        }
    }
}
