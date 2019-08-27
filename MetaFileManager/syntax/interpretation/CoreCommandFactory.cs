using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.lexer;
using Uroboros.syntax.interpretation.commands;

namespace Uroboros.syntax.interpretation
{
    class CoreCommandFactory
    {
        public static ICommand Build(List<Token> tokens, bool forced)
        {
            switch (tokens.First().GetTokenType())
            {
                case TokenType.Copy:
                {
                    if (TokenGroups.ContainsTokenOutsideBrackets(tokens, TokenType.To))
                    {
                        if (TokenGroups.ContainsTokenOutsideBrackets(tokens, TokenType.As))
                            return InterpreterCoreToAs.Build(tokens, forced);
                        else
                            return InterpreterCoreTo.Build(tokens, forced);
                    }
                    else
                        return InterpreterCore.Build(tokens);
                }
                case TokenType.Cut:
                {
                    if (TokenGroups.ContainsTokenOutsideBrackets(tokens, TokenType.To))
                    {
                        if (TokenGroups.ContainsTokenOutsideBrackets(tokens, TokenType.As))
                            return InterpreterCoreToAs.Build(tokens, forced);
                        else
                            return InterpreterCoreTo.Build(tokens, forced);
                    }
                    else
                        return InterpreterCore.Build(tokens);
                }
                case TokenType.CreateDirectory:
                {
                    return InterpreterCore.BuildCreate(tokens, forced, true);
                }
                case TokenType.CreateFile:
                {
                    return InterpreterCore.BuildCreate(tokens, forced, false);
                }
                case TokenType.Delete:
                {
                    return InterpreterCore.Build(tokens);
                }
                case TokenType.Drop:
                {
                    return InterpreterCore.Build(tokens);
                }
                case TokenType.Open:
                {
                    return InterpreterCore.Build(tokens);
                }
                case TokenType.Move:
                {
                    if (TokenGroups.ContainsTokenOutsideBrackets(tokens, TokenType.To))
                    {
                        if (TokenGroups.ContainsTokenOutsideBrackets(tokens, TokenType.As))
                            return InterpreterCoreToAs.Build(tokens, forced);
                        else
                            return InterpreterCoreTo.Build(tokens, forced);
                    }
                    else
                        throw new SyntaxErrorException("ERROR! Move command do not contain destination directory.");
                }
                case TokenType.Reaccess:
                {
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.To)))
                        return InterpreterCoreToTime.Build(tokens);
                    else
                        throw new SyntaxErrorException("ERROR! Reaccess command do not contain definition of new time.");
                }
                case TokenType.Recreate:
                {
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.To)))
                        return InterpreterCoreToTime.Build(tokens);
                    else
                        throw new SyntaxErrorException("ERROR! Recreate command do not contain definition of new time.");
                }
                case TokenType.Remodify:
                {
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.To)))
                        return InterpreterCoreToTime.Build(tokens);
                    else
                        throw new SyntaxErrorException("ERROR! Remodify command do not contain definition of new time.");
                }
                case TokenType.Rename:
                {
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.To)))
                        return InterpreterCoreTo.Build(tokens, forced);
                    else
                        throw new SyntaxErrorException("ERROR! Rename command do not contain definition of new name.");
                }
            }
            return null;
        }
    }
}
