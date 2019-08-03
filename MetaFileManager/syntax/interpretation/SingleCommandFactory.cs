using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.commands;
using Uroboros.syntax.interpretation.commands;
using Uroboros.syntax.interpretation.list;
using Uroboros.syntax.commands.other;

namespace Uroboros.syntax.interpretation
{
    class SingleCommandFactory
    {


        public static ICommand Build(List<Token> tokens)
        {
            bool forced = false;

            // remove 'force to' in the beginning
            if (tokens.First().GetTokenType().Equals(TokenType.ForceTo))
            {
                forced = true;
                tokens.RemoveAt(0);
                if (tokens.Count() == 0)
                    throw new SyntaxErrorException("ERROR! One command contains only two keywords: 'force to'. Instruction part is empty.");
            }

            // build two word commands
            if (tokens.Count == 2 && tokens.First().GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.Variable))
            {
                return InterTwoWordsCommand.Build(tokens.First().GetContent().ToLower(), tokens[1].GetContent().ToLower());
            }

            // build core command
            if (TokenGroups.IsCoreCommandKeyword(tokens.First().GetTokenType()))
            {
                return CoreCommandFactory.Build(tokens, forced);
            }

            // build commands which start from specified keyword
            switch (tokens.First().GetTokenType())
            {
                case TokenType.Add:
                {
                    return InterAdd.Build(tokens);
                }
                case TokenType.Order:
                {
                    if (tokens.Any(t => t.GetTokenType().Equals(TokenType.By)))
                        return InterOrder.Build(tokens);
                    else
                        throw new SyntaxErrorException("ERROR! Order command do not contain definition of sorting variables.");
                }
                case TokenType.Print:
                {
                    return InterPrint.Build(tokens.Skip(1).ToList());
                }
                case TokenType.Remove:
                {
                    return InterRemove.Build(tokens);
                }
                case TokenType.Reverse:
                {
                    return InterReverse.Build(tokens);
                }
                case TokenType.Select:
                {
                    return InterSelect.Build(tokens);
                }
                case TokenType.Sleep:
                {
                    return InterSleep.Build(tokens);
                }
            }

            // commands for variables
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
                if (TokenGroups.IsVariableOperation(tokens[1].GetTokenType()))
                {
                    return InterVariableOperation.Build(tokens);
                }
            }



            // finally - check if it can be 'print' command
            // this is the last possible command type
            return InterPrint.Build(tokens);
        }
    }
}
