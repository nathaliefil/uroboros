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
                        return null;
                }
                if (tokens[0].GetTokenType().Equals(TokenType.NumericConstant))
                    return new NumericConstant(tokens[0].GetNumericContent());
            }

            // try to build numeric function
            if (tokens.Count > 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.BracketOn)
                && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                INumerable inu = InterNumericFunction.Build(tokens);
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

                Logger.GetInstance().Log(tok.Print());
            }

            if (currentTokens.Count > 0)
            {
                INumerable ibo = NumerableBuilder.Build(currentTokens);
                if (!ibo.IsNull())
                    infixList.Add(ibo);
                else
                    return null;
            }

            // try to build inversion of one numerable
            if (infixList.Count == 2 && (infixList[0] is NumericExpressionOperator) && (infixList[1] is INumerable)
                && (infixList[0] as NumericExpressionOperator).GetOperatorType().Equals(NumericExpressionOperatorType.Minus))
            {
                return new NegatedNumerable(infixList[1] as INumerable);
            }

            // check if value of infixlist can be computed (check order of elements)
            if (!CheckExpressionComputability(infixList))
                return null;

            // if everything is right, finally build BoolExpression in RPN
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

        private static bool CheckExpressionComputability(List<INumericExpressionElement> infixList)
        {
            //todo
            return true;
        }

        private static List<INumericExpressionElement> ReversePolishNotation(List<INumericExpressionElement> infixList)
        {
            //todo
            return infixList;
        }
    }
}
