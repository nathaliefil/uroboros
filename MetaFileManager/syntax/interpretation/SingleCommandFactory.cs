using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.lexer;
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
                return InterpreterTwoWordsCommand.Build(tokens.First().GetContent().ToLower(), tokens[1].GetContent().ToLower());
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
                    return InterpreterAdd.Build(tokens);
                case TokenType.Order:
                    return InterpreterOrder.Build(tokens);
                case TokenType.Print:
                    return InterpreterPrint.Build(tokens.Skip(1).ToList());
                case TokenType.Remove:
                    return InterpreterRemove.Build(tokens);
                case TokenType.Reverse:
                    return InterpreterReverse.Build(tokens);
                case TokenType.Select:
                    return InterpreterSelect.Build(tokens);
                case TokenType.Sleep:
                    return InterpreterSleep.Build(tokens);
                case TokenType.Swap:
                    return InterpreterSwap.Build(tokens);
            }

            // commands for variables actualization
            if (tokens.Count >= 2 && tokens[0].GetTokenType().Equals(TokenType.Variable))
            {
                if (tokens[1].GetTokenType().Equals(TokenType.Equals))
                {
                    ICommand icom = InterpreterVariableDeclaration.Build(tokens);
                    if (!icom.IsNull())
                        return icom;
                }
                if (tokens[1].GetTokenType().Equals(TokenType.PlusPlus)
                    || tokens[1].GetTokenType().Equals(TokenType.MinusMinus))
                {
                    return InterpreterVariablePlusMinus.Build(tokens);
                }
                if (TokenGroups.IsVariableOperation(tokens[1].GetTokenType()))
                {
                    return InterpreterVariableOperation.Build(tokens);
                }
            }

            // command for changing one element of list variable
            if (tokens.Count >= 6 && tokens[0].GetTokenType().Equals(TokenType.Variable)
                && tokens[1].GetTokenType().Equals(TokenType.SquareBracketOn)
                && tokens.Where(t => t.GetTokenType() == TokenType.SquareBracketOff).Any())
            {
                ICommand icom = InterpreterVariableElement.Build(tokens);
                if (!icom.IsNull())
                    return icom;
            }

            // finally - check if it can be 'print' command
            // this is the last possible command type
            return InterpreterPrint.Build(tokens);
        }
    }
}
