using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.commands;

namespace Uroboros.syntax.interpretation.tokenlists
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
            List<Token> currentTokens = new List<Token>();

            for (int i = 0; i < tokens.Count; i++)
            {
                if (!tokens[i].GetTokenType().Equals(TokenType.Semicolon))
                {
                    currentTokens.Add(tokens[i].Clone());
                }
                else
                {
                    if (currentTokens.Count > 0)
                    {
                        commands.Add(SingleCommandFactory.Build(currentTokens));
                    }
                    currentTokens.Clear();
                }
            }
            if (currentTokens.Count > 0)
            {
                commands.Add(SingleCommandFactory.Build(currentTokens));
            }
            return commands;
        }
    }
}
