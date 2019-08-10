using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.commands.core;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterCore
    {
        public static ICommand Build(List<Token> tokens)
        {
            TokenType type = tokens[0].GetTokenType();
            tokens.RemoveAt(0);

            if (tokens.Count == 0)
                return BuildSimple(type);

            IListable ilist = ListableBuilder.Build(tokens);
            if (ilist.IsNull())
                throw new SyntaxErrorException("ERROR! There are is something wrong with elements declaration in " + GetName(type) + " command.");
            else
                return BuildComplex(type, ilist);
        }

        private static ICommand BuildSimple(TokenType type)
        {
            switch (type)
            {
                case TokenType.Copy:
                    return new Copy(new StringVariableRefer("this"));
                case TokenType.Cut:
                    return new Cut(new StringVariableRefer("this"));
                case TokenType.Delete:
                    return new Delete(new StringVariableRefer("this"));
                case TokenType.Drop:
                    return new Drop(new StringVariableRefer("this"));
                case TokenType.Open:
                    return new Open(new StringVariableRefer("this"));
            }
            throw new SyntaxErrorException("ERROR! Command not indentified."); // this is never thrown
        }

        private static ICommand BuildComplex(TokenType type, IListable ilist)
        {
            switch (type)
            {
                case TokenType.Copy:
                    return new Copy(ilist);
                case TokenType.Cut:
                    return new Cut(ilist);
                case TokenType.Delete:
                    return new Delete(ilist);
                case TokenType.Drop:
                    return new Drop(ilist);
                case TokenType.Open:
                    return new Open(ilist);
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
                case TokenType.Delete:
                    return "delete";
                case TokenType.Drop:
                    return "drop";
                case TokenType.Open:
                    return "open";
            }
            throw new SyntaxErrorException("ERROR! Command not indentified.");
        }

        public static ICommand BuildCreate(List<Token> tokens, bool forced, bool directory)
        {
            int froms = tokens.Where(x => x.GetTokenType().Equals(TokenType.From)).Count();
            tokens.RemoveAt(0);

            if (froms == 0)
                return BuildCreateUsual(tokens, forced, directory);
            if (froms == 1)
                return BuildCreateFrom(tokens, forced, directory);

            throw new SyntaxErrorException("ERROR! There are is something wrong with " + (directory ? "directory" : "file")
                + " creation command syntax.");
        }

        public static ICommand BuildCreateUsual(List<Token> tokens, bool forced, bool directory)
        {
            if (tokens.Count == 0)
            {
                if (directory)
                    return new CreateDirectory(new StringVariableRefer("this"), forced);
                else
                    return new CreateFile(new StringVariableRefer("this"), forced);
            }
            IListable ilist = ListableBuilder.Build(tokens);


            if (ilist.IsNull())
                throw new SyntaxErrorException("ERROR! There are is something wrong with " + (directory ? "directory" : "file")
                    + " creation command syntax.");
            if (directory)
                return new CreateDirectory(ilist, forced);
            else
                return new CreateFile(ilist, forced);
        }

        public static ICommand BuildCreateFrom(List<Token> tokens, bool forced, bool directory)
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
                    if (pastFrom)
                        part2.Add(tok);
                    else
                        part1.Add(tok);
                }
            }
            if (part2.Count == 0)
                throw new SyntaxErrorException("ERROR! Source in " + (directory ? "directory" : "file")
                    + " creation command is empty.");

            IListable ilist;
            IStringable istring = StringableBuilder.Build(part2);
            if (part1.Count == 0)
                ilist = new StringVariableRefer("this");
            else
                ilist = ListableBuilder.Build(part1);

            if (ilist.IsNull())
                throw new SyntaxErrorException("ERROR! There are is something wrong with new " + (directory ? "directory" : "file") +
                    " name in " + (directory ? "directory" : "file") + " creation command syntax.");
            if (istring.IsNull())
                throw new SyntaxErrorException("ERROR! There are is something wrong with source " + (directory ? "directory" : "file") +
                    " name in " + (directory ? "directory" : "file") + " creation command syntax.");

            if (directory)
                return new CreateDirectoryFrom(istring, ilist, forced);
            else
                return new CreateFileFrom(istring, ilist, forced);
        }
    }
}
