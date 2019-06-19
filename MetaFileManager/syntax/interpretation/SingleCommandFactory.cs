using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.commands;
using Uroboros.syntax.commands.abstracts;
using Uroboros.syntax.interpretation.commands;
using Uroboros.syntax.interpretation.list;

namespace Uroboros.syntax.interpretation
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
                    throw new SyntaxErrorException("ERROR! One command contains only two keywords: 'force to'. Instruction part is empty.");
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
                case TokenType.Print:
                {
                    return InterPrint.Build(tokens);
                }
                case TokenType.Reverse:
                {
                    return InterReverse.Build(tokens);
                }
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
                case TokenType.Sleep:
                {
                    return InterSleep.Build(tokens);
                }

                    // more more more
            }

            if (tokens.Count >= 2 && tokens[0].GetTokenType().Equals(TokenType.Variable))
            {
                if (tokens[1].GetTokenType().Equals(TokenType.Equals))
                {
                    return InterVariableDeclaration.Build(tokens);
                }
                if (tokens[1].GetTokenType().Equals(TokenType.PlusPlus) 
                    || tokens[1].GetTokenType().Equals(TokenType.MinusMinus))
                {
                    return InterVariablePlusMinus.Build(tokens);
                }
                if (tokens[1].GetTokenType().Equals(TokenType.PlusEquals)
                    || tokens[1].GetTokenType().Equals(TokenType.MinusEquals)
                    || tokens[1].GetTokenType().Equals(TokenType.MultiplyEquals)
                    || tokens[1].GetTokenType().Equals(TokenType.DivideEquals))
                {
                    return InterVariableOperation.Build(tokens);
                }
            }



            return new NullCommand ();
        }
    }
}
