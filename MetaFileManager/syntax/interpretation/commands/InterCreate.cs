using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.commands.abstracts;
using Uroboros.syntax.reading;
using Uroboros.syntax.commands.create;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;

namespace Uroboros.syntax.interpretation.commands
{
    class InterCreate
    {
        public static ICommand Build(List<Token> tokens, bool forced, bool directory)
        {
            int froms  = tokens.Where(x => x.GetTokenType().Equals(TokenType.From)).Count();
            tokens.RemoveAt(0);

            if (froms == 0)
                return BuildUsual(tokens, forced, directory);
            if (froms == 1)
                return BuildFrom(tokens, forced, directory);

            throw new SyntaxErrorException("ERROR! There are is something wrong with " + (directory ? "directory" : "file") 
                + " creation command syntax.");
        }

        public static ICommand BuildUsual(List<Token> tokens, bool forced, bool directory)
        {
            if (tokens.Count == 0)
            {
                if(directory)
                    return new CreateDirectory(new StringVariableRefer("this"), forced);
                else
                    return new CreateFile(new StringVariableRefer("this"), forced);
            }
            IStringable istring = StringableBuilder.Build(tokens);


            if (istring is NullVariable)
                throw new SyntaxErrorException("ERROR! There are is something wrong with " + (directory ? "directory" : "file") 
                    + " creation command syntax.");
            if (directory)
                return new CreateDirectory(istring, forced);
            else
                return new CreateFile(istring, forced);
        }

        public static ICommand BuildFrom(List<Token> tokens, bool forced, bool directory)
        {
            List<Token> part1 = new List<Token>();
            List<Token> part2 = new List<Token>();
            bool pastFrom = false;
            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.From))
                    pastFrom = true;
                else
                {
                    if(pastFrom)
                        part2.Add(tok);
                    else
                        part1.Add(tok);
                }
            }
            if (part2.Count == 0)
                throw new SyntaxErrorException("ERROR! Source in " + (directory ? "directory" : "file") 
                    + " creation command is empty.");

            IStringable istring1;
            IStringable istring2 = StringableBuilder.Build(part2);
            if (part1.Count == 0)
                istring1 = new StringVariableRefer("this");
            else
                istring1 = StringableBuilder.Build(part1);

            if (istring1 is NullVariable)
                throw new SyntaxErrorException("ERROR! There are is something wrong with new " + (directory ? "directory" : "file") +
                    " name in " + (directory ? "directory" : "file") + " creation command syntax.");
            if (istring2 is NullVariable)
                throw new SyntaxErrorException("ERROR! There are is something wrong with source " + (directory ? "directory" : "file") +
                    " name in " + (directory ? "directory" : "file") + " creation command syntax.");

            if (directory)
                return new CreateDirectoryFrom(istring2, istring1, forced);
            else
                return new CreateFileFrom(istring2, istring1, forced);
        }
    }
}
