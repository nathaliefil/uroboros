using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.expressions.bools.comparisons;
using Uroboros.syntax.interpretation.functions;
using Uroboros.syntax.expressions.bools;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.interpretation.expressions
{
    class BoolableBuilder
    {
        public static IBoolable Build(List<Token> tokens)
        {
            // check is is empty
            if (tokens.Count == 0)
                throw new SyntaxErrorException("ERROR! Variable declaration is empty.");

            // check if contains not allowed tokens
            Token wwtok = TokenGroups.WrongTokenInExpression(tokens);
            if (!wwtok.GetTokenType().Equals(TokenType.Null))
                return new NullVariable();

            // check brackets
            if (!Brackets.CheckCorrectness(tokens))
                return new NullVariable();

            // try to build simple one-element Boolable
            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.Bool))
                        return new BoolVariableRefer(str);
                    else
                        return new NullVariable();
                }
                if (tokens[0].GetTokenType().Equals(TokenType.BoolConstant))
                {
                    if (tokens[0].GetContent().Equals("true"))
                        return new BoolConstant(true);
                    else
                        return new BoolConstant(false);
                }
            }

            // try to build IN function
            if (tokens.Where(t => t.GetTokenType().Equals(TokenType.In)).Count() == 1)
            {
                IBoolable iboo = InBuilder.Build(tokens);
                if (!(iboo is NullVariable))
                    return iboo;
            }

            // try to build comparison = != > < >= <=
            if (ContainsOneComparingToken(tokens) && !ContainsLogicTokens(tokens))
            {
                IBoolable iboo = BuildComparison(tokens);
                if (!(iboo is NullVariable))
                    return iboo;
            }

            // try to build bool function
            if (tokens.Count > 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.BracketOn)
                && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                IBoolable iboo = InterBoolFunction.Build(tokens);
                if (!(iboo is NullVariable))
                    return iboo;
            }

            // try to build expression: many elements with operators or, and, xor, not
            if (ContainsLogicTokens(tokens))
                return BuildExpression(tokens);
            else
                return new NullVariable();
        }



        private static bool ContainsLogicTokens(List<Token> tokens)
        {
            return tokens.Where(x => TokenGroups.IsLogicSign(x.GetTokenType())).Any();
        }

        private static bool ContainsComparingTokens(List<Token> tokens)
        {
            return tokens.Where(x => TokenGroups.IsComparingSign(x.GetTokenType())).Any();
        }

        private static bool ContainsOneComparingToken(List<Token> tokens)
        {
            return tokens.Where(x => TokenGroups.IsComparingSign(x.GetTokenType())).Count() == 1;
        }
    
    
        private static IBoolable BuildComparison(List<Token> tokens)
        {
            int index = tokens.TakeWhile(x => !TokenGroups.IsComparingSign(x.GetTokenType())).Count();

            if (index == 0 || index == tokens.Count - 1)
                return new NullVariable();

            ComparisonType type = GetComparingToken(tokens);
            List<Token> leftTokens = tokens.GetRange(0, index);
            List<Token> rightTokens = tokens.GetRange(index + 1, tokens.Count - index - 1);
            IListable leftL = ListableBuilder.Build(leftTokens);
            IListable rightL = ListableBuilder.Build(rightTokens);

            if (leftL is NullVariable || rightL is NullVariable)
                return new NullVariable();

            if (leftL is INumerable && rightL is INumerable)
                return new NumericComparison(leftL as INumerable, rightL as INumerable, type);

            if (leftL is IStringable && rightL is IStringable)
                return new Uroboros.syntax.expressions.bools.comparisons.StringComparison(leftL as IStringable, rightL as IStringable, type);

            if (leftL is IListable && rightL is IListable)
                return new ListComparison(leftL as IListable, rightL as IListable, type);

            return new NullVariable();
        }

        private static IBoolable BuildExpression(List<Token> tokens)
        {
            // turn list of tokens into list of BoolExpressionElements
            // they are in usual infix notation
            // when this is done, their order is changed to Reverse Polish Notation
            // meanwhile check, if it all can be represented as simple one negated IBoolable
            // finally build BoolExpression

            List<IBoolExpressionElement> infixList = new List<IBoolExpressionElement>();
            List<Token> currentTokens = new List<Token>();
            bool readingFunction = false;
            Token previousToken = new Token(TokenType.Null);

            // first, merge many to tokens into fewer number of IBoolables
            foreach (Token tok in tokens)
            {
                bool actionDone = false;

                if (TokenGroups.IsLogicSign(tok.GetTokenType()))
                {
                    if (readingFunction)
                    {
                        if (Brackets.AllBracketsClosed(currentTokens))
                        {
                            IBoolable ibo = BoolableBuilder.Build(currentTokens);
                            if (!(ibo is NullVariable))
                                infixList.Add(ibo);
                            else
                                return new NullVariable();
                            currentTokens.Clear();
                            readingFunction = false;
                            infixList.Add(OperatorFromToken(tok));
                        }
                        else
                            currentTokens.Add(tok);
                    }
                    else
                    {
                        if (currentTokens.Count > 0)
                        {
                            IBoolable ibo = BoolableBuilder.Build(currentTokens);
                            if (!(ibo is NullVariable))
                                infixList.Add(ibo);
                            else
                                return new NullVariable();
                            currentTokens.Clear();
                        }
                        infixList.Add(new BoolExpressionOperator(GetBEOT(tok.GetTokenType())));
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
                                IBoolable ibo = BoolableBuilder.Build(currentTokens);
                                if (!(ibo is NullVariable))
                                    infixList.Add(ibo);
                                else
                                    return new NullVariable();
                                currentTokens.Clear();
                            }
                            infixList.Add(new BoolExpressionOperator(BoolExpressionOperatorType.BracketOn));
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
                            IBoolable ibo = BoolableBuilder.Build(currentTokens);
                            if (!(ibo is NullVariable))
                                infixList.Add(ibo);
                            else
                                return new NullVariable();
                            currentTokens.Clear();
                            
                            readingFunction = false;
                            infixList.Add(new BoolExpressionOperator(BoolExpressionOperatorType.BracketOff));
                        }
                        else
                            currentTokens.Add(tok);
                    }
                    else
                    {
                        if (currentTokens.Count > 0)
                        {
                            IBoolable ibo = BoolableBuilder.Build(currentTokens);
                            if (!(ibo is NullVariable))
                                infixList.Add(ibo);
                            else
                                return new NullVariable();
                            currentTokens.Clear();
                        }
                        infixList.Add(new BoolExpressionOperator(BoolExpressionOperatorType.BracketOff));
                    }
                    actionDone = true;
                }

                if (!actionDone)
                    currentTokens.Add(tok);

                previousToken = tok;
            }

            if (currentTokens.Count > 0)
            {
                IBoolable ibo = BoolableBuilder.Build(currentTokens);
                if (!(ibo is NullVariable))
                    infixList.Add(ibo);
                else
                    return new NullVariable();
            }

            // try to build negation of one boolable
            if (infixList.Count == 2 && (infixList[0] is BoolExpressionOperator) && (infixList[1] is IBoolable)
                && (infixList[0] as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not))
            {
                return new NegatedBoolable(infixList[1] as IBoolable);
            }
            
            // check if value of infixlist can be computed (check order of elements)
            if (!CheckExpressionComputability(infixList))
                return new NullVariable();

            // if everything is right, finally build BoolExpression in RPN
            return new BoolExpression(ReversePolishNotation(infixList));
        }

        private static bool CheckExpressionComputability(List<IBoolExpressionElement> infixList)
        {
            // check if expression is correct
            // done be checking neighboring elements
            // usual 'infix notation'
            // if infix notation is correct, postfix also should be

            if (infixList.Count == 0)
                return false;

            IBoolExpressionElement previous = infixList.First();

            /*
            CASE: previous == NOT
            > beel = NOT          wrong
            > beel = AND,OR,XOR   wrong
            > beel = bool         correct
            > beel = (            correct
            > beel = )            wrong

            CASE: previous == AND,OR,XOR
            > beel = NOT          correct
            > beel = AND,OR,XOR   wrong
            > beel = bool         correct
            > beel = (            correct
            > beel = )            wrong
            
            CASE: previous == bool
            > beel = NOT          wrong
            > beel = AND,OR,XOR   correct
            > beel = bool         wrong
            > beel = (            wrong
            > beel = )            correct
            
            CASE: previous == (
            > beel = NOT          correct
            > beel = AND,OR,XOR   wrong
            > beel = bool         correct
            > beel = (            correct
            > beel = )            correct
            
            CASE: previous == )
            > beel = NOT          wrong
            > beel = AND,OR,XOR   correct
            > beel = bool         wrong
            > beel = (            wrong
            > beel = )            correct
            
            FIRST ELEMENT
            > NOT          correct
            > AND,OR,XOR   wrong
            > bool         correct
            > (            correct
            > )            wrong

            LAST ELEMENT
            > NOT          wrong
            > AND,OR,XOR   wrong
            > bool         correct
            > (            wrong
            > )            correct
            
            implementation below
            */

            foreach (IBoolExpressionElement beel in infixList.Skip(1))
            {
                if (previous is BoolExpressionOperator)
                {
                    if ((previous as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not))
                    {
                        if (beel is BoolExpressionOperator &&
                        ((beel as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not)
                            || (beel as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOff)
                            || IsLogicBinaryOperator(beel as BoolExpressionOperator)))
                                return false;
                    }
                    else if ((previous as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOn))
                    {
                        if (beel is BoolExpressionOperator &&
                            IsLogicBinaryOperator(beel as BoolExpressionOperator))
                            return false;
                    }
                    else if ((previous as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOff))
                    {
                        if ((beel is BoolExpressionOperator &&
                        ((beel as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not)
                            || (beel as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOn)))
                            || beel is IBoolable)
                            return false;
                    }
                    else
                    {
                        if (beel is BoolExpressionOperator &&
                            ((beel as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOff)
                            || IsLogicBinaryOperator(beel as BoolExpressionOperator)))
                            return false;
                    }
                }

                if (previous is IBoolable)
                {
                    if ((beel is BoolExpressionOperator &&
                        ((beel as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not)
                            || (beel as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOn)))
                            || beel is IBoolable)
                        return false;
                }
                previous = beel;
            }

            if (infixList.First() is BoolExpressionOperator &&
                            ((infixList.First() as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOff)
                            || IsLogicBinaryOperator(infixList.First() as BoolExpressionOperator)))
                return false;

            if (infixList.Last() is BoolExpressionOperator &&
                        ((infixList.Last() as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not)
                            || (infixList.Last() as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOn)
                            || IsLogicBinaryOperator(infixList.Last() as BoolExpressionOperator)))
                return false;

            return true;
        }

        private static List<IBoolExpressionElement> ReversePolishNotation(List<IBoolExpressionElement> infixList)
        {
            // Dijkstra algo of convertion to Reverse Polish Notation

            Stack<BoolExpressionOperator> operatorStack = new Stack<BoolExpressionOperator>();
            List<IBoolExpressionElement> output = new List<IBoolExpressionElement>();

            foreach (IBoolExpressionElement ibee in infixList)
            {
                if (ibee is IBoolable)
                    output.Add(ibee);

                if (ibee is BoolExpressionOperator)
                {
                    if ((ibee as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.BracketOff))
                    {
                        while (operatorStack.Count > 0)
                        {
                            BoolExpressionOperator beo = operatorStack.Pop();

                            if (beo.GetOperatorType().Equals(BoolExpressionOperatorType.BracketOn))
                                break;
                            else
                                output.Add(beo as IBoolExpressionElement);
                        }
                    }
                    else
                        operatorStack.Push(ibee as BoolExpressionOperator);
                }
            }

            while (operatorStack.Count > 0)
                output.Add(operatorStack.Pop() as IBoolExpressionElement);

            return output;
        }

        private static BoolExpressionOperatorType GetBEOT(TokenType type)
        {
            switch (type)
            {
                case TokenType.And:
                    return BoolExpressionOperatorType.And;
                case TokenType.Or:
                    return BoolExpressionOperatorType.Or;
                case TokenType.Xor:
                    return BoolExpressionOperatorType.Xor;
                case TokenType.Exclamation:
                    return BoolExpressionOperatorType.Not;
            }
            return BoolExpressionOperatorType.And;
        }

        private static ComparisonType GetComparingToken(List<Token> tokens)
        {
            TokenType ttype = (tokens.Where(x => TokenGroups.IsComparingSign(x.GetTokenType())).First()).GetTokenType();

            switch (ttype)
            {
                case TokenType.Equals:
                    return ComparisonType.Equals;
                case TokenType.NotEquals:
                    return ComparisonType.NotEquals;
                case TokenType.Bigger:
                    return ComparisonType.Bigger;
                case TokenType.BiggerOrEquals:
                    return ComparisonType.BiggerOrEquals;
                case TokenType.Smaller:
                    return ComparisonType.Smaller;
                case TokenType.SmallerOrEquals:
                    return ComparisonType.SmallerOrEquals;
            }
            return ComparisonType.Equals; // is never returned
        }

        private static BoolExpressionOperator OperatorFromToken(Token tok)
        {
            switch (tok.GetTokenType())
            {
                case TokenType.And:
                    return new BoolExpressionOperator(BoolExpressionOperatorType.And);
                case TokenType.Or:
                    return new BoolExpressionOperator(BoolExpressionOperatorType.Or);
                case TokenType.Xor:
                    return new BoolExpressionOperator(BoolExpressionOperatorType.Xor);
                case TokenType.Exclamation:
                    return new BoolExpressionOperator(BoolExpressionOperatorType.Not);
            }
            return new BoolExpressionOperator(BoolExpressionOperatorType.Not);
        }

        public static bool IsLogicBinaryOperator(BoolExpressionOperator beo)
        {
            return TokenGroups.BINARY_LOGIC_OPERATOR.Contains(beo.GetOperatorType()) ? true : false;
        }
    }
}
