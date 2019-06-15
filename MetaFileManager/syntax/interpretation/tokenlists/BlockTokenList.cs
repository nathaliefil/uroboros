using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.commands;

namespace DivineScript.syntax.interpretation.tokenlists
{
    class BlockTokenList : ITokenList
    {
        List<Token> precedingTokens;
        List<TokenList> elements;

        public BlockTokenList(List<Token> precedingTokens, List<Token> tokens)
        {
            this.precedingTokens = precedingTokens;
            elements = new List<TokenList>();
            BuildItself();
        }

        public BlockTokenList(List<Token> tokens)
        {
            this.precedingTokens = new List<Token>();
            elements = new List<TokenList>();
            BuildItself();
        }

        private void BuildItself()
        {
            // here we go
        }

        public List<ICommand> ToCommands()
        {
            List<ICommand> commands = new List<ICommand>();

            // code

            return commands;
        }

    }
}
