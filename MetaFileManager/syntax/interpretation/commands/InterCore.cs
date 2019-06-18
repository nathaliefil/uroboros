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
    class InterCore
    {
        public static ICommand Build(List<Token> tokens)
        {
            TokenType type = tokens[0].GetTokenType();
            tokens.RemoveAt(0);

            if (tokens.Count == 0)
                return BuildSimple(type);

            IListable ilist = ListableBuilder.Build(tokens);
            if (ilist is NullVariable)
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
                case TokenType.Select:
                    return new Select(new StringVariableRefer("this"));
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
                case TokenType.Select:
                    return new Select(ilist);
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
                case TokenType.Select:
                    return "select";
            }
            return "one";
        }
    }
}
