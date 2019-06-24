using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.functions.bools;

namespace Uroboros.syntax.interpretation.functions
{
    class InterBoolFunction
    {
        public static IBoolable Build(List<Token> tokens)
        {
            if (Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
                return new NullVariable();

            List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();

            string name = tokensCopy[0].GetContent().ToLower();
            tokensCopy.RemoveAt(tokensCopy.Count - 1);
            tokensCopy.RemoveAt(0);
            tokensCopy.RemoveAt(0);

            List<Argument> args = ArgumentsExtractor.GetArguments(tokensCopy);

            if (name.Equals("exist") || name.Equals("exists"))
                return BuildStr(name, args);
            if (name.Equals("empty"))
                return BuildLis(name, args);
            if (name.Equals("contain") || name.Equals("contain2"))
                return BuildLisStr(name, args);

            return new NullVariable();
        }

        // functions are grouped by their arguments
        // every set of arguments is one method below

        public static IBoolable BuildStr(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 text argument.");

            IStringable istr = StringableBuilder.Build(args[0].tokens);
            if (istr is NullVariable)
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as text.");
            else
            {
                if (name.Equals("exist"))
                    return new FuncExist(istr);
                if (name.Equals("exists"))
                    return new FuncExist(istr);
                throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
            }
        }

        public static IBoolable BuildLis(string name, List<Argument> args)
        {
            if (args.Count != 1)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 1 list argument.");

            IListable ilis = ListableBuilder.Build(args[0].tokens);
            if (ilis is NullVariable)
                throw new SyntaxErrorException("ERROR! Argument of function " + name + " cannot be read as list.");
            else
            {
                if (name.Equals("empty"))
                    return new FuncEmpty(ilis);
                throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
            }
        }

        public static IBoolable BuildLisStr(string name, List<Argument> args)
        {
            if (args.Count != 2)
                throw new SyntaxErrorException("ERROR! Function " + name + " has to have 2 arguments: one list and one text.");

            IListable inu1 = ListableBuilder.Build(args[0].tokens);
            IStringable inu2 = StringableBuilder.Build(args[1].tokens);

            if (inu1 is NullVariable)
                throw new SyntaxErrorException("ERROR! First argument of function " + name + " cannot be read as list.");
            if (inu2 is NullVariable)
                throw new SyntaxErrorException("ERROR! Second argument of function " + name + " cannot be read as text.");

            if (name.Equals("contain"))
                return new FuncContain(inu1, inu2);
            if (name.Equals("contains"))
                return new FuncContain(inu1, inu2);
            throw new SyntaxErrorException("ERROR! Function " + name + " not identified.");
        }
    }
}
