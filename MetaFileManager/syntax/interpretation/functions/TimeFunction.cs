using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.lexer;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.functions.time;
using Uroboros.syntax.interpretation.functions.arg;

namespace Uroboros.syntax.interpretation.functions
{
    class TimeFunction
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
            else if (name.Equals("newyear") || name.Equals("christmas") || name.Equals("easter"))
                return BuildNum(name, args);
            else if (name.Equals("access") || name.Equals("creation") || name.Equals("modification"))
                return BuildStr(name, args);
            else if (name.Equals("tomorrow") || name.Equals("yesterday") || name.Equals("today"))
                return BuildWithArgs023(name, args);

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
            else if (name.Equals("tomorrow"))
                return new FuncTomorrow__3args(inu1, inu2, inu3);
            else if (name.Equals("yesterday"))
                return new FuncYesterday__3args(inu1, inu2, inu3);
            else if (name.Equals("today"))
                return new FuncToday__3args(inu1, inu2, inu3);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static ITimeable BuildNumNum(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 numeric arguments.");

            INumerable inu1 = NumerableBuilder.Build(args[0].tokens);
            INumerable inu2 = NumerableBuilder.Build(args[1].tokens);

            if (inu1.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as number.");
            if (inu2.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as number.");

            if (name.Equals("tomorrow"))
                return new FuncTomorrow__2args(inu1, inu2);
            else if (name.Equals("yesterday"))
                return new FuncYesterday__2args(inu1, inu2);
            else if (name.Equals("today"))
                return new FuncToday__2args(inu1, inu2);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }


        public static ITimeable BuildNum(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 numeric argument.");

            INumerable inu = NumerableBuilder.Build(args[0].tokens);

            if (inu.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as number.");

            if (name.Equals("newyear"))
                return new FuncNewyear(inu);
            else if (name.Equals("christmas"))
                return new FuncChristmas(inu);
            else if (name.Equals("easter"))
                return new FuncEaster(inu);

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static ITimeable BuildStr(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 text argument.");

            IStringable istr = StringableBuilder.Build(args[0].tokens);

            if (istr.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as text.");

            if (name.Equals("access"))
                return new FuncAccess(istr);
            else if (name.Equals("creation"))
                return new FuncCreation(istr);
            else if (name.Equals("modification"))
                return new FuncModification(istr);

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static ITimeable BuildEmpty(string name, List<Argument> args)
        {
            if (args.Count != 0)
                throw new SyntaxErrorException("ERROR! Function " + name + " cannot have arguments.");

            if (name.Equals("tomorrow"))
                return new FuncTomorrow__0args();
            else if (name.Equals("yesterday"))
                return new FuncYesterday__0args();
            else if (name.Equals("today"))
                return new FuncToday__0args();

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        // these functions can have 0, 2 or 3 arguments
        public static ITimeable BuildWithArgs023(string name, List<Argument> args)
        {
            if (args.Count == 0)
                return BuildEmpty(name, args);
            else if (args.Count == 2)
                return BuildNumNum(name, args);
            else if (args.Count == 3)
                return BuildNumNumNum(name, args);

            throw new SyntaxErrorException("ERROR! Function " + name + " has to have 0, 2 or 3 numeric arguments.");
        }
    }
}
