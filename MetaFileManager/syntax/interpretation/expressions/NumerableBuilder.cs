using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.interpretation.functions;
using Uroboros.syntax.expressions.numeric;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.interpretation.expressions
{
    class NumerableBuilder
    {
        public static INumerable Build(List<Token> tokens)
        {
            // try to build Boolable
            IBoolable ibo = BoolableBuilder.Build(tokens);
            if (!ibo.IsNull())
                return (ibo as INumerable);

            // try to build simple one-token Numerable
            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.Number))
                        return new NumericVariableRefer(str);
                    else
                    {
                        // try to build reference to element of time variable
                        INumerable inum = BuildTimeVariableRefer(tokens[0]);
                        if (!inum.IsNull())
                            return inum;
                    }
                }
                if (tokens[0].GetTokenType().Equals(TokenType.NumericConstant))
                    return new NumericConstant(tokens[0].GetNumericContent());
            }

            // try to build numeric function
            if (tokens.Count > 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.BracketOn)
                && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                INumerable inu = NumericFunction.Build(tokens);
                if (!inu.IsNull())
                    return inu;
            }

            // try to build expression: many elements with operators +, -, *, /, %
            if (ContainsArithmeticTokens(tokens))
                return BuildExpression(tokens);
            else
                return null;
        }

        private static bool ContainsArithmeticTokens(List<Token> tokens)
        {
            return tokens.Where(x => TokenGroups.IsArithmeticSign(x.GetTokenType())).Any();
        }

        private static INumerable BuildTimeVariableRefer(Token token)
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
                    case "year":
                        return new TimeElementRefer(leftSide, TimeVariableType.Year);
                    case "month":
                        return new TimeElementRefer(leftSide, TimeVariableType.Month);
                    case "day":
                        return new TimeElementRefer(leftSide, TimeVariableType.Day);
                    case "weekday":
                        return new TimeElementRefer(leftSide, TimeVariableType.WeekDay);
                    case "hour":
                        return new TimeElementRefer(leftSide, TimeVariableType.Hour);
                    case "minute":
                        return new TimeElementRefer(leftSide, TimeVariableType.Minute);
                    case "second":
                        return new TimeElementRefer(leftSide, TimeVariableType.Second);
                }
                return null;
            }
            else
                throw new SyntaxErrorException("ERROR! Variable " + leftSide + " do not exist.");
        }

        private static INumerable BuildExpression(List<Token> tokens)
        {
            // turn list of tokens into list of NumericExpressionElements
            // they are in usual infix notation
            // when this is done, their order is changed to Reverse Polish Notation
            // meanwhile check, if it all can be represented as simple one negated INumerable
            // finally build NumericExpression

            List<INumericExpressionElement> infixList = new List<INumericExpressionElement>();
            List<Token> currentTokens = new List<Token>();
            bool readingFunction = false;
            Token previousToken = new Token(TokenType.Null);

            // first, merge many tokens into fewer number of INumericExpressionElements
            foreach (Token tok in tokens)
            {
                bool actionDone = false;

                if (TokenGroups.IsArithmeticSign(tok.GetTokenType()))
                {
                    if (readingFunction)
                    {
                        if (Brackets.AllBracketsClosed(currentTokens))
                        {
                            INumerable ibo = NumerableBuilder.Build(currentTokens);
                            if (!ibo.IsNull())
                                infixList.Add(ibo);
                            else
                                return null;
                            currentTokens.Clear();
                            readingFunction = false;
                            infixList.Add(new NumericExpressionOperator(GetNEOT(tok.GetTokenType())));
                        }
                        else
                            currentTokens.Add(tok);
                    }
                    else
                    {
                        if (currentTokens.Count > 0)
                        {
                            INumerable ibo = NumerableBuilder.Build(currentTokens);
                            if (!ibo.IsNull())
                                infixList.Add(ibo);
                            else
                                return null;
                            currentTokens.Clear();
                        }
                        infixList.Add(new NumericExpressionOperator(GetNEOT(tok.GetTokenType())));
                    }
                    actionDone = true;
                }

                if (tok.GetTokenType().Equals(TokenType.BracketOn))
                {
                    if (readingFunction)
                        currentTokens.Add(tok);
                    else
                    {
                        if (currentTokens.Count == 1 && previousToken.GetTokenType().Equals(TokenType.Variable))
                        {
                            currentTokens.Add(tok);
                            readingFunction = true;
                        }
                        else
                        {
                            if (currentTokens.Count > 0)
                            {
                                INumerable ibo = NumerableBuilder.Build(currentTokens);
                                if (!ibo.IsNull())
                                    infixList.Add(ibo);
                                else
                                    return null;
                                currentTokens.Clear();
                            }
                            infixList.Add(new NumericExpressionOperator(NumericExpressionOperatorType.BracketOn));
                        }

                    }
                    actionDone = true;
                }

                if (tok.GetTokenType().Equals(TokenType.BracketOff))
                {
                    if (readingFunction)
                    {
                        if (Brackets.AllBracketsClosed(currentTokens))
                        {
                            INumerable ibo = NumerableBuilder.Build(currentTokens);
                            if (!ibo.IsNull())
                                infixList.Add(ibo);
                            else
                                return null;
                            currentTokens.Clear();

                            readingFunction = false;
                            infixList.Add(new NumericExpressionOperator(NumericExpressionOperatorType.BracketOff));
                        }
                        else
                            currentTokens.Add(tok);
                    }
                    else
                    {
                        if (currentTokens.Count > 0)
                        {
                            INumerable ibo = NumerableBuilder.Build(currentTokens);
                            if (!ibo.IsNull())
                                infixList.Add(ibo);
                            else
                                return null;
                            currentTokens.Clear();
                        }
                        infixList.Add(new NumericExpressionOperator(NumericExpressionOperatorType.BracketOff));
                    }
                    actionDone = true;
                }

                if (!actionDone)
                    currentTokens.Add(tok);

                previousToken = tok;
            }

            if (currentTokens.Count > 0)
            {
                INumerable ibo = NumerableBuilder.Build(currentTokens);
                if (!ibo.IsNull())
                    infixList.Add(ibo);
                else
                    return null;
            }

            // try to build inversion of one INumerable
            if (infixList.Count == 2 && (infixList[0] is NumericExpressionOperator) && (infixList[1] is INumerable)
                && (infixList[0] as NumericExpressionOperator).GetOperatorType().Equals(NumericExpressionOperatorType.Minus))
            {
                return BuildNegated(infixList[1] as INumerable);
            }

            // change unary minuses to new type to avoid mistaking them with subtraction sign
            infixList = IdentifyUnaryMinuses(infixList);

            // check if value of infixlist can be computed (check order of elements)
            if (!CheckExpressionComputability(infixList))
                return null;

            // if everything is right, finally build NumericExpression in RPN
            return new NumericExpression(ReversePolishNotation(infixList));
        }

        private static NumericExpressionOperatorType GetNEOT(TokenType type)
        {
            switch (type)
            {
                case TokenType.Plus:
                    return NumericExpressionOperatorType.Plus;
                case TokenType.Minus:
                    return NumericExpressionOperatorType.Minus;
                case TokenType.Multiply:
                    return NumericExpressionOperatorType.Multiply;
                case TokenType.Divide:
                    return NumericExpressionOperatorType.Divide;
                case TokenType.Percent:
                    return NumericExpressionOperatorType.Modulo;
            }
            return NumericExpressionOperatorType.Plus;
        }

        private static List<INumericExpressionElement> IdentifyUnaryMinuses(List<INumericExpressionElement> infixList)
        {
            for (int i = 0; i < infixList.Count; i++)
            {
                if ((infixList[i] is NumericExpressionOperator) 
                    && (infixList[i] as NumericExpressionOperator).GetOperatorType().Equals(NumericExpressionOperatorType.Minus))
                {
                    if (i == 0 || !(infixList[i - 1] is INumerable))
                        (infixList[i] as NumericExpressionOperator).UnaryMinus();
                }
            }
            return infixList;
        }

        private static bool CheckExpressionComputability(List<INumericExpressionElement> infixList)
        {
            //todo
            return true;
        }

        private static List<INumericExpressionElement> ReversePolishNotation(List<INumericExpressionElement> infixList)
        {
            // Dijkstra algo of convertion to Reverse Polish Notation
            // without operation order
            /// todo

            Stack<NumericExpressionOperator> operatorStack = new Stack<NumericExpressionOperator>();
            List<INumericExpressionElement> output = new List<INumericExpressionElement>();

            // reverse order of elements so can be read left-to-right
            // includes reversing brackets
            /*infixList.Reverse();
            foreach (INumericExpressionElement bee in infixList)
            {
                if (bee is NumericExpressionOperator)
                    (bee as NumericExpressionOperator).ReverseBracket();
            }*/

            foreach (INumericExpressionElement ibee in infixList)
            {
                if (ibee is INumerable)
                    output.Add(ibee);

                if (ibee is NumericExpressionOperator)
                {
                    if ((ibee as NumericExpressionOperator).GetOperatorType().Equals(NumericExpressionOperatorType.BracketOn))
                    {
                        operatorStack.Push(ibee as NumericExpressionOperator);
                    }
                    if ((ibee as NumericExpressionOperator).GetOperatorType().Equals(NumericExpressionOperatorType.BracketOff))
                    {
                        while (operatorStack.Count > 0 && !operatorStack.Peek().GetOperatorType().Equals(NumericExpressionOperatorType.BracketOn))
                            output.Add(operatorStack.Pop());


                        operatorStack.Pop();
                    }
                    else if ((ibee as NumericExpressionOperator).GetOperatorType().Equals(NumericExpressionOperatorType.UnaryMinus))
                    {
                        operatorStack.Push(ibee as NumericExpressionOperator);
                    }
                    else
                    {
                        while (operatorStack.Count > 0 && TopOfStackHasPriority(operatorStack.Peek(), ibee as NumericExpressionOperator))
                            output.Add(operatorStack.Pop());


                        operatorStack.Push(ibee as  NumericExpressionOperator);
                    }
                }
            }

            while (operatorStack.Count > 0)
                output.Add(operatorStack.Pop() as INumericExpressionElement);


            //test
            foreach (INumericExpressionElement inee in output)
            {
                Logger.GetInstance().Log(inee.ToString().Length > 10 ? inee.ToString().Substring(36) : inee.ToString());
            }

            //test

            return output;
        }

        private static bool TopOfStackHasPriority(NumericExpressionOperator topOfStack, NumericExpressionOperator newOperator)
        {
            int stackPriority = GetPriority(topOfStack);
            int newOperatorPriority = GetPriority(newOperator);
            
            return newOperatorPriority < stackPriority;
        }

        private static bool NewOperatorIsEquivalent(NumericExpressionOperator topOfStack, NumericExpressionOperator newOperator)
        {
            int stackPriority = GetPriority(topOfStack);
            int newOperatorPriority = GetPriority(newOperator);

            return newOperatorPriority == stackPriority;
        }

        private static int GetPriority(NumericExpressionOperator neo)
        {
            switch (neo.GetOperatorType())
            {
                case NumericExpressionOperatorType.Modulo:
                    return 2;
                case NumericExpressionOperatorType.Multiply:
                    return 2;
                case NumericExpressionOperatorType.Divide:
                    return 2;
                case NumericExpressionOperatorType.Plus:
                    return 1;
                case NumericExpressionOperatorType.Minus:
                    return 1;
            }
            return 0;
        }

        public static INumerable BuildNegated(INumerable inum)
        {
            if (inum is NumericConstant)
            {
                (inum as NumericConstant).SetNegative();
                return inum;
            }
            else
                return new NegatedNumerable(inum);
        }
    }
}
