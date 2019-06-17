using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.commands;

namespace DivineScript.syntax.interpretation.tokenlists
{
    class TokenList
    {
        private List<Token> tokens;

        public TokenList(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public virtual List<ICommand> ToCommands()
        {
            return BuildSingleCommands();
        }

        protected List<ICommand> BuildSingleCommands()
        {
            List<ICommand> commands = new List<ICommand>();



            return commands;
        }
    }
}
