using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.lexer;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.commands.var;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterVariableElement
    {
        public static ICommand Build(List<Token> tokens)
        {
            string name = tokens[0].GetContent();
            tokens.RemoveAt(0);
            tokens.RemoveAt(0);

            List<Token> indexTokens = new List<Token>();
            while (tokens.First().GetTokenType() != TokenType.SquareBracketOff)
            {
                indexTokens.Add(tokens.First());
                tokens.RemoveAt(0);
            }
            tokens.RemoveAt(0);

            INumerable index = NumerableBuilder.Build(indexTokens);
            if (index.IsNull())
                throw new SyntaxErrorException("ERROR! Index of element of list " + name + " cannot be read as number.");
            if (tokens.First().GetTokenType() != TokenType.Equals)
                return null;

            tokens.RemoveAt(0);

            IStringable newValue = StringableBuilder.Build(tokens);
            if (newValue.IsNull())
                return null;

            return new ListElementDeclaration(name, newValue, index);
        }
    }
}
