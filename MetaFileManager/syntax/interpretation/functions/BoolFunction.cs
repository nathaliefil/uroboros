﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.lexer;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.functions.bools;
using Uroboros.syntax.interpretation.functions.arg;

namespace Uroboros.syntax.interpretation.functions
{
    class BoolFunction
    {
        public static IBoolable Build(List<Token> tokens)
        {
            if (Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
                return null;

            List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();

            string name = tokensCopy[0].GetContent().ToLower();
            tokensCopy.RemoveAt(tokensCopy.Count - 1);
            tokensCopy.RemoveAt(0);
            tokensCopy.RemoveAt(0);

            List<Argument> args = ArgumentsExtractor.GetArguments(tokensCopy);

            if (name.Equals("exist") || name.Equals("exists") || name.Equals("empty")
                || name.Equals("emptydirectory") || name.Equals("iscorrect")
                || name.Equals("isdirectory") || name.Equals("isfile")
                || name.Equals("hidden") || name.Equals("readonly"))
                return BuildStr(name, args);
            else if (name.Equals("existinside"))
                return BuildStrStr(name, args);
            else if (name.Equals("emptylist") || name.Equals("listisempty"))
                return BuildLis(name, args);
            else if (name.Equals("contain") || name.Equals("contains"))
                return BuildLisStr(name, args);
            else if (name.Equals("samedate") || name.Equals("samedates")
                || name.Equals("thesamedate") || name.Equals("thesamedates")
                || name.Equals("sameclock") || name.Equals("sameclocks")
                || name.Equals("thesameclock") || name.Equals("thesameclocks"))
                return BuildTimTim(name, args);

            return null;
        }

        // functions are grouped by their arguments
        // every set of arguments is one method below

        public static IBoolable BuildStr(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 text argument.");

            IStringable istr = StringableBuilder.Build(args[0].tokens);
            if (istr.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as text.");
            if (name.Equals("exist") || name.Equals("exists"))
                return new FuncExist(istr);
            else if (name.Equals("empty") || name.Equals("emptydirectory"))
                return new FuncEmpty(istr);
            else if (name.Equals("iscorrect"))
                return new FuncIscorrect(istr);
            else if (name.Equals("isdirectory"))
                return new FuncIsdirectory(istr);
            else if (name.Equals("isfile"))
                return new FuncIsfile(istr);
            else if (name.Equals("hidden"))
                return new FuncHidden(istr);
            else if (name.Equals("readonly"))
                return new FuncReadonly(istr);

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static IBoolable BuildStrStr(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 text arguments.");

            IStringable istr1 = StringableBuilder.Build(args[0].tokens);
            IStringable istr2 = StringableBuilder.Build(args[1].tokens);

            if (istr1.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as text.");
            if (istr2.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as text.");

            if (name.Equals("existinside") || name.Equals("existsinside"))
                return new FuncExistinside(istr1, istr2);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static IBoolable BuildTimTim(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 time arguments.");

            ITimeable itim1 = TimeableBuilder.Build(args[0].tokens);
            ITimeable itim2 = TimeableBuilder.Build(args[1].tokens);

            if (itim1.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as time.");
            if (itim2.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as time.");

            if (name.Equals("samedate") || name.Equals("samedates")
                || name.Equals("thesamedate") || name.Equals("thesamedates"))
                return new FuncSamedates(itim1, itim2);
            else if (name.Equals("sameclock") || name.Equals("sameclocks")
                || name.Equals("thesameclock") || name.Equals("thesameclocks"))
                return new FuncSamedates(itim1, itim2);

            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static IBoolable BuildLis(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 list argument.");

            IListable ilis = ListableBuilder.Build(args[0].tokens);
            if (ilis.IsNull())
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as list.");

            if (name.Equals("emptylist") || name.Equals("listisempty"))
                return new FuncEmptylist(ilis);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }

        public static IBoolable BuildLisStr(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 arguments: one list and one text.");

            IListable ilis = ListableBuilder.Build(args[0].tokens);
            IStringable istr = StringableBuilder.Build(args[1].tokens);

            if (ilis.IsNull())
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as list.");
            if (istr.IsNull())
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as text.");

            if (name.Equals("contain") || name.Equals("contains"))
                return new FuncContain(ilis, istr);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }
    }
}
