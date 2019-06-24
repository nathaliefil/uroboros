using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.variables.refers;

namespace Uroboros.syntax.interpretation.functions
{
    class InterListElement
    {
        public static IStringable Build(List<Token> tokens)
        {
            bool singlePairOfBrackets = tokens.Where(x => x.GetTokenType().Equals(TokenType.SquareBracketOn)).Count() == 1
                && tokens.Where(x => x.GetTokenType().Equals(TokenType.SquareBracketOff)).Count() == 1;

            string name = tokens[0].GetContent();
            tokens.RemoveAt(tokens.Count-1);
            tokens.RemoveAt(0);
            tokens.RemoveAt(0);

            if (!InterVariables.GetInstance().Contains(name, InterVarType.List))
                if (singlePairOfBrackets)
                    throw new SyntaxErrorException("ERROR! List " + name + " not found. Impossible to take element from it.");
                else 
                    return new NullVariable();

            INumerable inu = NumerableBuilder.Build(tokens);
            if (inu is NullVariable)
                if (singlePairOfBrackets)
                    throw new SyntaxErrorException("ERROR! Impossible to take element from list " + name + ". Index identificator cannot be read as number.");
                else
                    return new NullVariable();
            else
                return new ListElementRefer(name, inu);
        }
    }
}
