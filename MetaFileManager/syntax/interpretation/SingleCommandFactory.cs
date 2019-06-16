using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.commands;
using DivineScript.syntax.commands.abstracts;
using DivineScript.syntax.interpretation.commands;

namespace DivineScript.syntax.interpretation
{
    class SingleCommandFactory
    {
        public static ICommand Build(List<Token> tokens)
        {
            bool forced = false;

            if (tokens.First().GetTokenType().Equals(TokenType.ForceTo))
            {
                forced = true;
                tokens.RemoveAt(0);
                if (tokens.Count() == 0)
                {
                    throw new SyntaxErrorException("ERROR! One command contains only two keywords: 'force to'.");
                }
            }

            switch (tokens.First().GetTokenType())
            {
                case TokenType.CreateDirectory:
                {
                    return InterCreate.Build(tokens, forced, true);
                }
                case TokenType.CreateFile:
                {
                    return InterCreate.Build(tokens, forced, false);
                }

            }


            return new NullCommand ();
        }
    }
}
