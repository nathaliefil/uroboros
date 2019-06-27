using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.functions.numeric;

namespace Uroboros.syntax.interpretation.functions
{
    class InterStringFunction
    {
        public static IStringable Build(List<Token> tokens)
        {
            if (Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
                return null;

            List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();

            string name = tokensCopy[0].GetContent().ToLower();
            tokensCopy.RemoveAt(tokensCopy.Count - 1);
            tokensCopy.RemoveAt(0);
            tokensCopy.RemoveAt(0);

            List<Argument> args = ArgumentsExtractor.GetArguments(tokensCopy);

            if (name.Equals("letter") || name.Equals("hex") || name.Equals("binary"))
                return BuildNum(name, args);
            if (name.Equals("substring"))
                return BuildSubstring(name, args);
            if (name.Equals("upper") || name.Equals("lower") || name.Equals("digits") || name.Equals("trim"))
                return BuildStr(name, args);
            if (name.Equals("filled") || name.Equals("fill"))
                return BuildStrNum(name, args);

            return null;
        }

        // functions are grouped by their arguments
        // every set of arguments is one method below
        // exception for substring - has it's own methods

        public static IStringable BuildNum(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 numeric argument.");

            INumerable inu = NumerableBuilder.Build(args[0].tokens);
            if (inu.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as number.");
            else
            {
                if (name.Equals("letter"))
                    return new FuncLetter(inu);
                if (name.Equals("hex"))
                    return new FuncHex(inu);
                if (name.Equals("binary"))
                    return new FuncBinary(inu);
                throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
            }
        }

        public static IStringable BuildStr(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 text argument.");

            IStringable istr = StringableBuilder.Build(args[0].tokens);
            if (istr.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as text.");
            else
            {
                if (name.Equals("upper"))
                    return new FuncUpper(istr);
                if (name.Equals("lower"))
                    return new FuncLower(istr);
                if (name.Equals("digits"))
                    return new FuncDigits(istr);
                if (name.Equals("trim"))
                    return new FuncTrim(istr);
                throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
            }
        }

        public static IStringable BuildStrNum(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 arguments: one text and one number.");

            IStringable inu1 = StringableBuilder.Build(args[0].tokens);
            INumerable inu2 = NumerableBuilder.Build(args[1].tokens);

            if (inu1.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as text.");
            if (inu2.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as number.");

            if (name.Equals("filled") || name.Equals("fill"))
                return new FuncFilled(inu1, inu2);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }


        public static IStringable BuildSubstring(string name, List<Argument> args)
        {
            if (args.Count < 2 || args.Count > 3)
                throw new SyntaxErrorException("ERROR! Function substring has to have 2 or 3 arguments.");

            if (args.Count == 2)
                return BuildSubstringWithTwoArgs(args);
            else
                return BuildSubstringWithThreeArgs(args);
        }

        public static IStringable BuildSubstringWithTwoArgs(List<Argument> args)
        {
            IStringable istr = StringableBuilder.Build(args[0].tokens);
            INumerable inu = NumerableBuilder.Build(args[1].tokens);

            if (istr.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function substring cannot be read as text.");
            if (inu.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function substring cannot be read as number.");

            return new FuncSubstring(istr, inu);
        }

        public static IStringable BuildSubstringWithThreeArgs(List<Argument> args)
        {
            IStringable istr = StringableBuilder.Build(args[0].tokens);
            INumerable inu1 = NumerableBuilder.Build(args[1].tokens);
            INumerable inu2 = NumerableBuilder.Build(args[2].tokens);

            if (istr.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function substring cannot be read as text.");
            if (inu1.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function substring cannot be read as number.");
            if (inu2.IsNull())
                throw new SyntaxErrorException("ERROR! Third argument of function substring cannot be read as number.");

            return new FuncSubstring(istr, inu1, inu2);
        }
    }
}
