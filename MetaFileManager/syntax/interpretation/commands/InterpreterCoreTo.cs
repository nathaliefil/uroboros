using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.lexer;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.commands.core;
using Uroboros.syntax.variables.refers;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterCoreTo
    {
        public static ICommand Build(List<Token> tokens, bool forced)
        {
            TokenType type = tokens[0].GetTokenType();
            tokens.RemoveAt(0);

            if (tokens.Where(t => t.GetTokenType().Equals(TokenType.To)).Count() > 1)
                throw new SyntaxErrorException("ERROR! In " + GetName(type) + " command keyword 'to' occurs too many times.");

            List<Token> part1 = new List<Token>();
            List<Token> part2 = new List<Token>();
            bool pastTo = false;
            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.To))
                    pastTo = true;
                else
                {
                    if (pastTo)
                        part2.Add(tok);
                    else
                        part1.Add(tok);
                }
            }

            if (part2.Count == 0)
                throw new SyntaxErrorException("ERROR! Command " + GetName(type) + " is too short and do not contain all necessary information.");

            IStringable expression2 = StringableBuilder.Build(part2);

            if(expression2.IsNull())
                throw new SyntaxErrorException("ERROR! Second part of command " + GetName(type) + " cannot be read as text.");

            if (part1.Count == 0)
                return BuildSimple(type, expression2, forced);
            else
            {
                IListable expression1 = ListableBuilder.Build(part1);
                if (expression1.IsNull())
                    throw new SyntaxErrorException("ERROR! First part of command " + GetName(type) + " cannot be read as list.");
                return BuildComplex(type, expression1, expression2, forced);
            }
        }

        private static ICommand BuildSimple(TokenType type, IStringable destination, bool forced)
        {
            switch (type)
            {
                case TokenType.Copy:
                    return new CopyTo(new StringVariableRefer("this"), destination, forced);
                case TokenType.Cut:
                    return new MoveTo(new StringVariableRefer("this"), destination, forced);
                case TokenType.Move:
                    return new MoveTo(new StringVariableRefer("this"), destination, forced);
                case TokenType.Rename:
                    return new RenameTo(new StringVariableRefer("this"), destination, forced);
            }
            throw new SyntaxErrorException("ERROR! Command not indentified."); // this is never thrown
        }

        private static ICommand BuildComplex(TokenType type, IListable ilist, IStringable destination, bool forced)
        {
            switch (type)
            {
                case TokenType.Copy:
                    return new CopyTo(ilist, destination, forced);
                case TokenType.Cut:
                    return new MoveTo(ilist, destination, forced);
                case TokenType.Move:
                    return new MoveTo(ilist, destination, forced);
                case TokenType.Rename:
                    return new RenameTo(ilist, destination, forced);
            }
            throw new SyntaxErrorException("ERROR! Command not indentified."); // this is never thrown
        }

        private static string GetName(TokenType type)
        {
            switch (type)
            {
                case TokenType.Copy:
                    return "copy";
                case TokenType.Cut:
                    return "cut";
                case TokenType.Move:
                    return "move";
                case TokenType.Rename:
                    return "rename";
            }
            return "one";
        }
    }
}
