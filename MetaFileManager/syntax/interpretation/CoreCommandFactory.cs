using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
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
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.To)))
                        return InterCoreTo.Build(tokens, forced);
                    else
                        return InterCore.Build(tokens);
                }
                case TokenType.Cut:
                {
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.To)))
                        return InterCoreTo.Build(tokens, forced);
                    else
                        return InterCore.Build(tokens);
                }
                case TokenType.CreateDirectory:
                {
                    return InterCore.BuildCreate(tokens, forced, true);
                }
                case TokenType.CreateFile:
                {
                    return InterCore.BuildCreate(tokens, forced, false);
                }
                case TokenType.Delete:
                {
                    return InterCore.Build(tokens);
                }
                case TokenType.Drop:
                {
                    return InterCore.Build(tokens);
                }
                case TokenType.Open:
                {
                    return InterCore.Build(tokens);
                }
                case TokenType.Select:
                {
                    return InterCore.Build(tokens);
                }
                case TokenType.Move:
                {
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.To)))
                        return InterCoreTo.Build(tokens, forced);
                    else
                        throw new SyntaxErrorException("ERROR! Move command do not contain destination directory.");
                }
                case TokenType.Rename:
                {
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.To)))
                        return InterCoreTo.Build(tokens, forced);
                    else
                        throw new SyntaxErrorException("ERROR! Rename command do not contain definition of new name.");
                }
            }
            return null;
        }
    }
}
