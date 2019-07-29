using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.functions.time;

namespace Uroboros.syntax.interpretation.functions
{
    class InterTimeFunction
    {
        public static ITimeable Build(List<Token> tokens)
        {
            if (Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
                return null;

            List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();

            string name = tokensCopy[0].GetContent().ToLower();
            tokensCopy.RemoveAt(tokensCopy.Count - 1);
            tokensCopy.RemoveAt(0);
            tokensCopy.RemoveAt(0);

            List<Argument> args = ArgumentsExtractor.GetArguments(tokensCopy);

            if (name.Equals("date"))
                return BuildNumNumNum(name, args);

            return null;
        }

        // functions are grouped by their arguments
        // every set of arguments is one method below


        public static ITimeable BuildNumNumNum(string name, List<Argument> args)
        {
            if (args.Count != 3)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 3 numeric arguments.");

            INumerable inu1 = NumerableBuilder.Build(args[0].tokens);
            INumerable inu2 = NumerableBuilder.Build(args[1].tokens);
            INumerable inu3 = NumerableBuilder.Build(args[2].tokens);

            if (inu1.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as number.");
            if (inu2.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as number.");
            if (inu3.IsNull())
                throw new SyntaxErrorException("ERROR! Third argument of function " + name + " cannot be read as number.");

            if (name.Equals("date"))
                return new FuncDate(inu1, inu2, inu3);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }
    }
}
