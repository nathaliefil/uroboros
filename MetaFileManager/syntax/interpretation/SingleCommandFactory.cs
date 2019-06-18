using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.commands;
using DivineScript.syntax.commands.abstracts;
using DivineScript.syntax.interpretation.commands;
using DivineScript.syntax.interpretation.list;

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
                    throw new SyntaxErrorException("ERROR! One command contains only two keywords: 'force to'.");
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
                    return InterCore.Build(tokens);
                }
                case TokenType.Cut:
                {
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
