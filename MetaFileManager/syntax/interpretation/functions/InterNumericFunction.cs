using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.functions.numeric.abstracts;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.functions.numeric;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.interpretation.functions
{
    class InterNumericFunction
    {
        public static INumerable Build(List<Token> tokens)
        {
            bool singlePairOfBrackets = tokens.Where(x => x.GetTokenType().Equals(TokenType.BracketOn)).Count() == 1
                && tokens.Where(x => x.GetTokenType().Equals(TokenType.BracketOff)).Count() == 1;

            List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();

            string name = tokensCopy[0].GetContent().ToLower();
            tokensCopy.RemoveAt(tokensCopy.Count - 1);
            tokensCopy.RemoveAt(0);
            tokensCopy.RemoveAt(0);

            List<Argument> args = ArgumentsExtractor.GetArguments(tokensCopy);

            if (name.Equals("round") || name.Equals("floor") || name.Equals("ceil"))
                return BuildNum(name, args, singlePairOfBrackets);
            if (name.Equals("power"))
                return BuildNumNum(name, args, singlePairOfBrackets);
            if (name.Equals("pi") || name.Equals("e"))
                return BuildEmpty(name, args, singlePairOfBrackets);
            if (name.Equals("number") || name.Equals("length"))
                return BuildStr(name, args, singlePairOfBrackets);
            if (name.Equals("min") || name.Equals("max"))
                return BuildNums(name, args, singlePairOfBrackets);
            if (name.Equals("count"))
                return BuildLis(name, args, singlePairOfBrackets);

            return new NullVariable();
        }

        // functions are grouped by their arguments
        // every set of arguments is one method below

        public static INumerable BuildNum(string name, List<Argument> args, bool singlePairOfBrackets)
        {
            if (args.Count != 1)
                return Error("ERROR! Function " + name + " has to have 1 numeric argument.", singlePairOfBrackets);

            INumerable inu = NumerableBuilder.Build(args[0].tokens);
            if (inu is NullVariable)
                return Error("ERROR! Argument of function " + name + " cannot be read as number.", singlePairOfBrackets);
            else
            {
                if (name.Equals("round"))
                    return new FuncRound(inu);
                if (name.Equals("floor"))
                    return new FuncRound(inu);
                if (name.Equals("ceil"))
                    return new FuncRound(inu);
                return Error("ERROR! Function " + name + " not identified.", singlePairOfBrackets);
            }
        }

        public static INumerable BuildNumNum(string name, List<Argument> args, bool singlePairOfBrackets)
        {
            if (args.Count != 2)
                return Error("ERROR! Function " + name + " has to have 2 numeric arguments.", singlePairOfBrackets);

            INumerable inu1 = NumerableBuilder.Build(args[0].tokens);
            INumerable inu2 = NumerableBuilder.Build(args[1].tokens);

            if (inu1 is NullVariable)
                return Error("ERROR! First argument of function " + name + " cannot be read as number.", singlePairOfBrackets);
            if (inu2 is NullVariable)
                return Error("ERROR! Second argument of function " + name + " cannot be read as number.", singlePairOfBrackets);

            if (name.Equals("power"))
                return new FuncPower(inu1, inu2);
            return Error("ERROR! Function " + name + " not identified.", singlePairOfBrackets);
        }

        public static INumerable BuildEmpty(string name, List<Argument> args, bool singlePairOfBrackets)
        {
            if (args.Count != 0)
                return Error("ERROR! Function " + name + " cannot have arguments.", singlePairOfBrackets);

            if (name.Equals("pi"))
                return new FuncPi();
            if (name.Equals("e"))
                return new FuncE();

            return Error("ERROR! Function " + name + " not identified.", singlePairOfBrackets);
        }

        public static INumerable BuildStr(string name, List<Argument> args, bool singlePairOfBrackets)
        {
            if (args.Count != 1)
                return Error("ERROR! Function " + name + " has to have 1 text argument.", singlePairOfBrackets);

            IStringable istr = StringableBuilder.Build(args[0].tokens);
            if (istr is NullVariable)
                return Error("ERROR! Argument of function " + name + " cannot be read as text.", singlePairOfBrackets);
            else
            {
                if (name.Equals("length"))
                    return new FuncLength(istr);
                return Error("ERROR! Function " + name + " not identified.", singlePairOfBrackets);
            }
        }

        public static INumerable BuildNums(string name, List<Argument> args, bool singlePairOfBrackets)
        {
            if (args.Count == 0)
                return Error("ERROR! Function " + name + " has to have at least one numeric argument.", singlePairOfBrackets);

            List<INumerable> inus = new List<INumerable>();

            for (int i = 0; i < args.Count; i++)
            {
                INumerable inu = NumerableBuilder.Build(args[i].tokens);
                if (inu is NullVariable)
                    return Error("ERROR! Argument " + (i + 1) + " of function " + name + " cannot be read as number.", singlePairOfBrackets);
                else
                    inus.Add(inu);
            }

            if (name.Equals("max"))
                return new FuncMax(inus);
            if (name.Equals("min"))
                return new FuncMin(inus);

            return Error("ERROR! Function " + name + " not identified.", singlePairOfBrackets);
        }

        public static INumerable BuildLis(string name, List<Argument> args, bool singlePairOfBrackets)
        {
            if (args.Count != 1)
                return Error("ERROR! Function " + name + " has to have 1 list argument.", singlePairOfBrackets);

            IListable ilis = ListableBuilder.Build(args[0].tokens);
            if (ilis is NullVariable)
                return Error("ERROR! Argument of function " + name + " cannot be read as list.", singlePairOfBrackets);
            else
            {
                if (name.Equals("count"))
                    return new FuncCount(ilis);
                return Error("ERROR! Function " + name + " not identified.", singlePairOfBrackets);
            }
        }

        private static INumerable Error(string message, bool singlePairOfBrackets)
        {
            if (singlePairOfBrackets)
                throw new SyntaxErrorException(message);
            else
                return new NullVariable();
        }
    }
}
