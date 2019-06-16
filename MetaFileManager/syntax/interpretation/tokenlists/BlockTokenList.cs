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
        List<ITokenList> elements;
        bool precedings;

        public BlockTokenList(List<Token> precedingTokens, List<Token> tokens)
        {
            this.precedingTokens = precedingTokens;
            precedings = true;
            BuildItself();
        }

        public BlockTokenList(List<Token> tokens)
        {
            precedings = false;
            BuildItself();
        }

        private void BuildItself()
        {
            elements = new List<ITokenList>();
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
