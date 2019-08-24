using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.lexer;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.functions.numeric;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables;
using Uroboros.syntax.interpretation.functions.arg;

namespace Uroboros.syntax.interpretation.functions
{
    class NumericFunction
    {
        public static INumerable Build(List<Token> tokens)
        {
            if (Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
                return null;

            List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();

            string name = tokensCopy[0].GetContent().ToLower();
            tokensCopy.RemoveAt(tokensCopy.Count - 1);
            tokensCopy.RemoveAt(0);
            tokensCopy.RemoveAt(0);

            List<Argument> args = ArgumentsExtractor.GetArguments(tokensCopy);

            if (name.Equals("round") || name.Equals("floor") || name.Equals("ceil") || name.Equals("ceiling")
                || name.Equals("sqrt") || name.Equals("ln") || name.Equals("log")
                || name.Equals("log10") || name.Equals("kb") || name.Equals("mb")
                || name.Equals("gb") || name.Equals("tb") || name.Equals("pb") || name.Equals("k")
                || name.Equals("kk") || name.Equals("factorial"))
                return BuildNum(name, args);
            else if (name.Equals("power") || name.Equals("pow"))
                return BuildNumNum(name, args);
            else if (name.Equals("pi") || name.Equals("e") || name.Equals("goldenratio"))
                return BuildEmpty(name, args);
            else if (name.Equals("number") || name.Equals("length") || name.Equals("year") || name.Equals("size"))
                return BuildStr(name, args);
            else if (name.Equals("min") || name.Equals("max") || name.Equals("average") || name.Equals("avg")
                || name.Equals("mean") || name.Equals("sum") || name.Equals("product"))
                return BuildNums(name, args);
            else if (name.Equals("count") || name.Equals("lengthofshortest") || name.Equals("lengthoflongest"))
                return BuildLis(name, args);
            else if (name.Equals("indexof"))
                return BuildStrStr(name, args);
            else if (name.Equals("yearday") || name.Equals("dayofyear"))
                return BuildTim(name, args);
            else if (name.Equals("yearsbetween") || name.Equals("monthsbetween") || name.Equals("daysbetween")
                || name.Equals("hoursbetween") || name.Equals("minutesbetween") || name.Equals("secondsbetween"))
                return BuildTimTim(name, args);

            return null;
        }

        // functions are grouped by their arguments
        // every set of arguments is one method below

        public static INumerable BuildNum(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 numeric argument.");

            INumerable inu = NumerableBuilder.Build(args[0].tokens);
            if (inu.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as number.");
            if (name.Equals("round"))
                return new FuncRound(inu);
            else if (name.Equals("floor"))
                return new FuncFloor(inu);
            else if (name.Equals("ceil") || name.Equals("ceiling"))
                return new FuncCeil(inu);
            else if (name.Equals("sqrt"))
                return new FuncSqrt(inu);
            else if (name.Equals("factorial"))
                return new FuncFactorial(inu);
            else if (name.Equals("ln") || name.Equals("log"))
                return new FuncLn(inu);
            else if (name.Equals("log10"))
                return new FuncLog10(inu);
            else if (name.Equals("k"))
                return new Func__SizeUnit(inu, SizeSufix.K);
            else if (name.Equals("kk"))
                return new Func__SizeUnit(inu, SizeSufix.KK);
            else if (name.Equals("kb"))
                return new Func__SizeUnit(inu, SizeSufix.KB);
            else if (name.Equals("mb"))
                return new Func__SizeUnit(inu, SizeSufix.MB);
            else if (name.Equals("gb"))
                return new Func__SizeUnit(inu, SizeSufix.GB);
            else if (name.Equals("tb"))
                return new Func__SizeUnit(inu, SizeSufix.TB);
            else if (name.Equals("pb"))
                return new Func__SizeUnit(inu, SizeSufix.PB);

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildNumNum(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 numeric arguments.");

            INumerable inu1 = NumerableBuilder.Build(args[0].tokens);
            INumerable inu2 = NumerableBuilder.Build(args[1].tokens);

            if (inu1.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as number.");
            if (inu2.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as number.");

            if (name.Equals("power") || name.Equals("pow"))
                return new FuncPower(inu1, inu2);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildStrStr(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 arguments: two texts.");

            IStringable istr1 = StringableBuilder.Build(args[0].tokens);
            IStringable istr2 = StringableBuilder.Build(args[1].tokens);

            if (istr1.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as text.");
            if (istr2.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as text.");

            if (name.Equals("indexof"))
                return new FuncIndexof(istr1, istr2);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildTimTim(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 time arguments.");

            ITimeable itim1 = TimeableBuilder.Build(args[0].tokens);
            ITimeable itim2 = TimeableBuilder.Build(args[1].tokens);

            if (itim1.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as time.");
            if (itim2.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as time.");

            if (name.Equals("yearsbetween"))
                return new Func__TimeBetween(itim1, itim2, TimeVariableType.Year);
            else if (name.Equals("monthsbetween"))
                return new Func__TimeBetween(itim1, itim2, TimeVariableType.Month);
            else if (name.Equals("daysbetween"))
                return new Func__TimeBetween(itim1, itim2, TimeVariableType.Day);
            else if (name.Equals("hoursbetween"))
                return new Func__TimeBetween(itim1, itim2, TimeVariableType.Hour);
            else if (name.Equals("minutesbetween"))
                return new Func__TimeBetween(itim1, itim2, TimeVariableType.Minute);
            else if (name.Equals("secondsbetween"))
                return new Func__TimeBetween(itim1, itim2, TimeVariableType.Second);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildEmpty(string name, List<Argument> args)
        {
            if (args.Count != 0)
                throw new SyntaxErrorException("ERROR! Function " + name + " cannot have arguments.");

            if (name.Equals("pi"))
                return new FuncPi();
            else if (name.Equals("e"))
                return new FuncE();
            else if (name.Equals("goldenratio"))
                return new FuncGoldenratio();

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildStr(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 text argument.");

            IStringable istr = StringableBuilder.Build(args[0].tokens);
            if (istr.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as text.");

            if (name.Equals("number"))
                return new FuncNumber(istr);
            if (name.Equals("length"))
                return new FuncLength(istr);
            if (name.Equals("year"))
                return new FuncYear(istr);
            if (name.Equals("size"))
                return new FuncSize(istr);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildNums(string name, List<Argument> args)
        {
            if (args.Count == 0)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have at least one numeric argument.");

            List<INumerable> inus = new List<INumerable>();

            for (int i = 0; i < args.Count; i++)
            {
                INumerable inu = NumerableBuilder.Build(args[i].tokens);
                if (inu.IsNull())
                    throw new SyntaxErrorException("ERROR! Argument " + (i + 1) + " of function " + name + " cannot be read as number.");
                else
                    inus.Add(inu);
            }

            if (name.Equals("max"))
                return new FuncMax(inus);
            else if (name.Equals("min"))
                return new FuncMin(inus);
            else if (name.Equals("average") || name.Equals("avg") || name.Equals("mean"))
                return new FuncAverage(inus);
            else if (name.Equals("sum"))
                return new FuncSum(inus);
            else if (name.Equals("product"))
                return new FuncProduct(inus);

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildLis(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 list argument.");

            IListable ilis = ListableBuilder.Build(args[0].tokens);
            if (ilis.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as list.");

            if (name.Equals("count"))
                return new FuncCount(ilis);
            else if (name.Equals("lengthofshortest"))
                return new FuncLengthofshortest(ilis);
            else if (name.Equals("lengthoflongest"))
                return new FuncLengthoflongest(ilis);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static INumerable BuildTim(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 time argument.");

            ITimeable itim = TimeableBuilder.Build(args[0].tokens);
            if (itim.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as time.");

            if (name.Equals("yearday") || name.Equals("dayofyear"))
                return new FuncYearday(itim);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");

        }
    }
}
