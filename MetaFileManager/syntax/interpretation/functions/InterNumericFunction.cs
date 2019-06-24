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
            if (Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
                return new NullVariable();

            List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();

            string name = tokensCopy[0].GetContent().ToLower();
            tokensCopy.RemoveAt(tokensCopy.Count - 1);
            tokensCopy.RemoveAt(0);
            tokensCopy.RemoveAt(0);

            List<Argument> args = ArgumentsExtractor.GetArguments(tokensCopy);

            if (name.Equals("round") || name.Equals("floor") || name.Equals("ceil"))
                return BuildNum(name, args);
            if (name.Equals("power"))
                return BuildNumNum(name, args);
            if (name.Equals("pi") || name.Equals("e"))
                return BuildEmpty(name, args);
            if (name.Equals("number") || name.Equals("length"))
                return BuildStr(name, args);
            if (name.Equals("min") || name.Equals("max"))
                return BuildNums(name, args);
            if (name.Equals("count"))
                return BuildLis(name, args);

            return new NullVariable();
        }

        // functions are grouped by their arguments
        // every set of arguments is one method below

        public static INumerable BuildNum(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 numeric argument.");

            INumerable inu = NumerableBuilder.Build(args[0].tokens);
            if (inu is NullVariable)
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as number.");
            else
            {
                if (name.Equals("round"))
                    return new FuncRound(inu);
                if (name.Equals("floor"))
                    return new FuncRound(inu);
                if (name.Equals("ceil"))
                    return new FuncRound(inu);
                throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
            }
        }

        public static INumerable BuildNumNum(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 numeric arguments.");

            INumerable inu1 = NumerableBuilder.Build(args[0].tokens);
            INumerable inu2 = NumerableBuilder.Build(args[1].tokens);

            if (inu1 is NullVariable)
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as number.");
            if (inu2 is NullVariable)
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as number.");

            if (name.Equals("power"))
                return new FuncPower(inu1, inu2);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildEmpty(string name, List<Argument> args)
        {
            if (args.Count != 0)
                throw new SyntaxErrorException("ERROR! Function " + name + " cannot have arguments.");

            if (name.Equals("pi"))
                return new FuncPi();
            if (name.Equals("e"))
                return new FuncE();

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildStr(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 text argument.");

            IStringable istr = StringableBuilder.Build(args[0].tokens);
            if (istr is NullVariable)
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as text.");
            else
            {
                if (name.Equals("length"))
                    return new FuncLength(istr);
                throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
            }
        }

        public static INumerable BuildNums(string name, List<Argument> args)
        {
            if (args.Count == 0)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have at least one numeric argument.");

            List<INumerable> inus = new List<INumerable>();

            for (int i = 0; i < args.Count; i++)
            {
                INumerable inu = NumerableBuilder.Build(args[i].tokens);
                if (inu is NullVariable)
                    throw new SyntaxErrorException("ERROR! Argument " + (i + 1) + " of function " + name + " cannot be read as number.");
                else
                    inus.Add(inu);
            }

            if (name.Equals("max"))
                return new FuncMax(inus);
            if (name.Equals("min"))
                return new FuncMin(inus);

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildLis(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 list argument.");

            IListable ilis = ListableBuilder.Build(args[0].tokens);
            if (ilis is NullVariable)
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as list.");
            else
            {
                if (name.Equals("count"))
                    return new FuncCount(ilis);
                throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
            }
        }
    }
}