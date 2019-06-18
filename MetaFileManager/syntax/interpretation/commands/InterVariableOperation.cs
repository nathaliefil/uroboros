using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.commands.arithmetic;

namespace Uroboros.syntax.interpretation.commands
{
    class InterVariableOperation
    {
        public static ICommand Build(List<Token> tokens)
        {
            string name = tokens[0].GetContent();
            TokenType type = tokens[1].GetTokenType();
            tokens.RemoveAt(0);
            tokens.RemoveAt(0);

            if (InterVariables.GetInstance().ContainsChangable(name, InterVarType.Number) &&
                !InterVariables.GetInstance().Contains(name, InterVarType.Bool))
            {
                INumerable value = NumerableBuilder.Build(tokens);
                if (value is NullVariable)
                    throw new SyntaxErrorException("ERROR! Changing value of variable " + name + " cannot be performed, because expression value is not a number.");

                switch (type)
                {
                    case TokenType.PlusEquals:
                        return new IncrementBy(name, value);
                    case TokenType.MinusEquals:
                        return new DecrementBy(name, value);
                    case TokenType.MultiplyEquals:
                        return new MultiplyBy(name, value);
                    case TokenType.DivideEquals:
                        return new DivideBy(name, value);
                }
            }
            throw new SyntaxErrorException("ERROR! Value of variable " + name + " cannot be changed.");
        }
    }
}
