using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.expressions.strings;
using Uroboros.syntax.interpretation.functions;
using Uroboros.syntax.variables.constants;

namespace Uroboros.syntax.interpretation.expressions
{
    class StringableBuilder
    {
        public static IStringable Build(List<Token> tokens)
        {
            // try to build Numerable
            INumerable inu = NumerableBuilder.Build(tokens);
            if (!inu.IsNull())
                return (inu as IStringable);

            // try to build Timeable
            ITimeable itim = TimeableBuilder.Build(tokens);
            if (!itim.IsNull())
                return (itim as IStringable);

            // remove first and last bracket if it is there
            while (tokens[0].GetTokenType().Equals(TokenType.BracketOn) && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff) &&
                !Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
            {
                List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();
                tokensCopy.RemoveAt(tokens.Count - 1);
                tokensCopy.RemoveAt(0);
                tokens = tokensCopy;
            }

            // try to build simple one-token Stringable
            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.String))
                        return new StringVariableRefer(str);
                    else
                    {
                        // try to build reference to date or clock time
                        IStringable istr = BuildTimeVariableRefer(tokens[0]);
                        if (!istr.IsNull())
                            return istr;
                    }
                }
                if (tokens[0].GetTokenType().Equals(TokenType.StringConstant))
                    return new StringConstant(tokens[0].GetContent());
            }

            //try to build string function
            if (tokens.Count > 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.BracketOn)
                && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                IStringable istr = StringFunction.Build(tokens);
                if (!istr.IsNull())
                    return istr;
            }

            // try to build string ternary
            if (TernaryBuilder.IsPossibleTernary(tokens))
            {
                IStringable istr = TernaryBuilder.BuildStringTernary(tokens);
                if (!istr.IsNull())
                    return istr;
            }

            // try to build reference to n-th element of list of strings
            if (tokens.Count > 3 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.SquareBracketOn)
                && tokens[tokens.Count-1].GetTokenType().Equals(TokenType.SquareBracketOff))
            {
                IStringable istr = ListElement.Build(tokens);
                if (!istr.IsNull())
                    return istr;
            }

            // try to build concatenated string -> text merged by +
            if(tokens.Any(t => t.GetTokenType().Equals(TokenType.Plus)))
                return BuildConcatenated(tokens);
            else
                return null;
        }


        private static IStringable BuildTimeVariableRefer(Token token)
        {
            string name = token.GetContent();
            int count = name.Count(c => c == '.');

            if (count == 0)
                return null;

            if (count > 1)
                throw new SyntaxErrorException("ERROR! Variable " + name + " contains multiple dot signs and because of that misguides compiler.");

            string leftSide = name.Substring(0, name.IndexOf('.')).ToLower();
            string rightSide = name.Substring(name.IndexOf('.') + 1).ToLower();

            if (leftSide.Length == 0 || rightSide.Length == 0)
                return null;

            if (InterVariables.GetInstance().Contains(leftSide, InterVarType.Time))
            {
                switch (rightSide)
                {
                    case "date":
                        return new TimeDateRefer(leftSide);
                    case "clock":
                        return new TimeClockRefer(leftSide);
                }
                return null;
            }
            else
                throw new SyntaxErrorException("ERROR! Variable " + leftSide + " do not exist.");
        }


        // string concatenation
        // considers numeric expressions inside with signs '+'
        public static IStringable BuildConcatenated(List<Token> tokens)
        {
            List<Token> currentTokens = new List<Token>();
            List<Token> reserve = new List<Token>();
            List<IStringable> elements = new List<IStringable>();
            int level = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].GetTokenType().Equals(TokenType.BracketOn))
                    level++;
                if (tokens[i].GetTokenType().Equals(TokenType.BracketOff))
                    level--;

                if (tokens[i].GetTokenType().Equals(TokenType.Plus) && level == 0)
                {
                    if (currentTokens.Count > 0)
                    {
                        IStringable ist = StringableBuilder.Build(currentTokens);

                        if (ist.IsNull())
                            return null;

                        if (ist is INumerable || ist is IBoolable)
                        {
                            reserve.AddRange(currentTokens);
                            reserve.Add(tokens[i]);
                            currentTokens.Clear();
                        }
                        else
                        {
                            if (reserve.Count > 0)
                            {
                                reserve.RemoveAt(reserve.Count - 1);
                                elements.Add(NumerableBuilder.Build(reserve) as IStringable);
                                reserve.Clear();
                            }

                            elements.Add(ist);
                            currentTokens.Clear();
                        }
                    }
                }
                else
                    currentTokens.Add(tokens[i]);
            }

            if (currentTokens.Count > 0)
            {
                IStringable ist = StringableBuilder.Build(currentTokens);

                if (reserve.Count > 0)
                {
                    if (ist is INumerable || ist is IBoolable)
                    {
                        reserve.AddRange(currentTokens);
                        elements.Add(NumerableBuilder.Build(reserve) as IStringable);
                    }
                    else
                    {
                        reserve.RemoveAt(reserve.Count - 1);
                        elements.Add(NumerableBuilder.Build(reserve) as IStringable);
                        elements.Add(ist);
                    }
                }
                else
                {
                    
                    if (ist.IsNull())
                        return null;
                    else
                        elements.Add(ist);
                }
                
            }

            if (elements.Count > 0)
                return new StringExpression(elements);

            return null;
        }
    }
}
