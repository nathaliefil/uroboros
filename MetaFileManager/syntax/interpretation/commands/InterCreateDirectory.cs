using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.commands;
using DivineScript.syntax.commands.abstracts;
using DivineScript.syntax.reading;

namespace DivineScript.syntax.interpretation.commands
{
    class InterCreateDirectory
    {
        public static ICommand Build(List<Token> tokens, bool forced)
        {
            int froms  = tokens.Where(x => x.GetTokenType().Equals(TokenType.From)).Count();

            if (froms == 0)
            {
                return BuildUsual(tokens, forced);
            }
            if (froms == 1)
            {
                return BuildFrom(tokens, forced);
            }
            throw new SyntaxErrorException("ERROR! There are too many appearances of keyword 'from' in directory creation command.");
        }

        public static ICommand BuildUsual(List<Token> tokens, bool forced)
        {
            //todo

            return new NullCommand();
        }

        public static ICommand BuildFrom(List<Token> tokens, bool forced)
        {
            // todo
            return new NullCommand();
        }
    }
}
