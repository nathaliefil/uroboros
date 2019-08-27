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
    class InterpreterCoreToAs
    {
        public static ICommand Build(List<Token> tokens, bool forced)
        {
            TokenType type = tokens[0].GetTokenType();
            tokens.RemoveAt(0);

            int toIndex = TokenGroups.IndexOfTokenOutsideBrackets(tokens, TokenType.To);
            int asIndex = TokenGroups.IndexOfTokenOutsideBrackets(tokens, TokenType.As);

            if (asIndex < toIndex)
                return null;
            if (toIndex == asIndex - 1)
                throw new SyntaxErrorException("ERROR! Command " + GetName(type) + " do not have definition of destination directory.");
            if (asIndex == tokens.Count - 1)
                throw new SyntaxErrorException("ERROR! Command " + GetName(type) + " do not have definition of new name for file/directory.");

            List<Token> listTokens = tokens.Take(toIndex).ToList();
            List<Token> destinationTokens = tokens.GetRange(toIndex + 1, asIndex - toIndex - 1);
            List<Token> nameTokens = tokens.Skip(asIndex + 1).ToList();

            IStringable destination = StringableBuilder.Build(destinationTokens);
            if (destination.IsNull())
                throw new SyntaxErrorException("ERROR! In command " + GetName(type) + " definition of destination directory cannot be read as text.");
            IStringable name = StringableBuilder.Build(nameTokens);
            if (name.IsNull())
                throw new SyntaxErrorException("ERROR! In command " + GetName(type) + " definition of new name for file/directory cannot be read as text.");

            if (listTokens.Count == 0)
                return BuildSimple(type, destination, name, forced);
            else
            {
                IListable list = ListableBuilder.Build(listTokens);
                if (list.IsNull())
                    throw new SyntaxErrorException("ERROR! In command " + GetName(type) + " definition of list of files and directories is not correct.");
                return BuildComplex(type, list, destination, name, forced);
            }
        }

        private static ICommand BuildSimple(TokenType type, IStringable destination, IStringable newName, bool forced)
        {
            switch (type)
            {
                case TokenType.Copy:
                    return new CopyToAs(new StringVariableRefer("this"),destination, newName, forced);
                case TokenType.Cut:
                    return new MoveToAs(new StringVariableRefer("this"), destination, newName, forced);
                case TokenType.Move:
                    return new MoveToAs(new StringVariableRefer("this"), destination, newName, forced);
            }
            throw new SyntaxErrorException("ERROR! Command not indentified."); // this is never thrown
        }

        private static ICommand BuildComplex(TokenType type, IListable ilist, IStringable destination, IStringable newName, bool forced)
        {
            switch (type)
            {
                case TokenType.Copy:
                    return new CopyToAs(ilist, destination, newName, forced);
                case TokenType.Cut:
                    return new MoveToAs(ilist, destination, newName, forced);
                case TokenType.Move:
                    return new MoveToAs(ilist, destination, newName, forced);
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
            }
            return "one";
        }
    }
}
