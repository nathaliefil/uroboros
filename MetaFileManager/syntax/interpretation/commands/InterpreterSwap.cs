using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.lexer;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.commands.var;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterSwap
    {
        public static ICommand Build(List<Token> tokens)
        {
            tokens.RemoveAt(0);

            if (tokens.Count() > 3)
                throw new SyntaxErrorException("ERROR! Command 'swap' is too long.");

            if (tokens.Count() < 3)
                throw new SyntaxErrorException("ERROR! Command 'swap' do not contain all necessary information.");

            if (!tokens[1].GetTokenType().Equals(TokenType.With) && !tokens[1].GetTokenType().Equals(TokenType.And))
                throw new SyntaxErrorException("ERROR! Command 'swap' do not have variables separated by keyword 'with' or 'and'.");

            if (!tokens[0].GetTokenType().Equals(TokenType.Variable))
                throw new SyntaxErrorException("ERROR! Command 'swap' do not have name of first variable to swap.");

            if (!tokens[2].GetTokenType().Equals(TokenType.Variable))
                throw new SyntaxErrorException("ERROR! Command 'swap' do not have name of second variable to swap.");

            string leftVariable = tokens[2].GetContent();
            string rightVariable = tokens[0].GetContent();

            if (!InterVariables.GetInstance().Contains(leftVariable))
                throw new SyntaxErrorException("ERROR! In command 'swap' variable " + leftVariable + " do not exist.");

            if (!InterVariables.GetInstance().Contains(rightVariable))
                throw new SyntaxErrorException("ERROR! In command 'swap' variable " + rightVariable + " do not exist.");

            InterVarType leftType = InterVariables.GetInstance().GetVarType(leftVariable);
            InterVarType rightType = InterVariables.GetInstance().GetVarType(rightVariable);

            if (!leftType.Equals(rightType))
                throw new SyntaxErrorException("ERROR! Variables " + leftVariable + " and " + rightVariable + " cannot be swapped, because they are of different type.");

            return new Swap(leftVariable, rightVariable);
        }
    }
}
