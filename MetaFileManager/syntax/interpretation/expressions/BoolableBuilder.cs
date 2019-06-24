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
            if (ContainsOneComparingToken(tokens))
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
            /*if (tokens.Where(x => TokenGroups.IsLogicSign(x.GetTokenType())).Any())
                return BuildExpression(tokens);
            else*/
                return new NullVariable();
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
            // meanwhile check, if it all they can be represented as simple negated IBoolable

            List<IBoolExpressionElement> infixList = new List<IBoolExpressionElement>();
            List<Token> currentTokens = new List<Token>();
            bool readingFunction = false;
            int level = 0;
            int functionLevel = 0;
            Token previousToken = new Token(TokenType.Null);

            foreach (Token tok in tokens)
            {
                bool actionDone = false;

                if (TokenGroups.IsLogicSign(tok.GetTokenType()))
                {
                    if (readingFunction)
                    {
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
                            infixList.Add(new BoolExpressionOperator(GetBEOT(tok.GetTokenType())));
                            currentTokens.Clear();
                        }
                    }
                    actionDone = true;
                }

                if (tok.GetTokenType().Equals(TokenType.BracketOn))
                {
                    if (readingFunction)
                        currentTokens.Add(tok);
                    else
                    {
                        if (previousToken.GetTokenType().Equals(TokenType.Variable))
                        {
                            functionLevel = level;
                            readingFunction = true;
                            level++;
                            currentTokens.Add(tok);
                        }
                        else
                        {
                            if (!TokenGroups.IsLogicSign(previousToken.GetTokenType()))
                                return new NullVariable();

                            IBoolable ibo = BoolableBuilder.Build(currentTokens);
                            if (!(ibo is NullVariable))
                                infixList.Add(ibo);
                            else
                                return new NullVariable();
                            infixList.Add(new BoolExpressionOperator(BoolExpressionOperatorType.BracketOn));
                            currentTokens.Clear();
                        }
                    }
                    level++;
                    actionDone = true;
                }

                if (tok.GetTokenType().Equals(TokenType.BracketOff))
                {
                    level--;

                    if (readingFunction)
                    {
                        currentTokens.Add(tok);

                        if (level == functionLevel)
                        {
                            IBoolable ibo = BoolableBuilder.Build(currentTokens);
                            if (!(ibo is NullVariable))
                                infixList.Add(ibo);
                            else
                                return new NullVariable();

                            currentTokens.Clear();
                        }
                    }
                    else
                    {
                        IBoolable ibo = BoolableBuilder.Build(currentTokens);
                        if (!(ibo is NullVariable))
                            infixList.Add(ibo);
                        else
                            return new NullVariable();
                        infixList.Add(new BoolExpressionOperator(BoolExpressionOperatorType.BracketOff));
                        currentTokens.Clear();
                    }
                    actionDone = true;
                }

                if (!actionDone)
                {
                    currentTokens.Add(tok);
                }
                previousToken = tok;
            }

            if (infixList.Count == 2 && (infixList[0] is BoolExpressionOperator) && (infixList[1] is IBoolable)
                && (infixList[0] as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not))
            {
                return new NegatedBoolable(infixList[1] as IBoolable);
            }
            
            // more more more
            // here we go
            /// todo

            return new NullVariable();
        }

        private static bool ContainsOneComparingToken(List<Token> tokens)
        {
            int count = tokens.Where(x => TokenGroups.IsComparingSign(x.GetTokenType())).Count();
            return count == 1 ? true : false;
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
    }
}
