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
using Uroboros.syntax.expressions.bools.other;

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
                return null;

            // check brackets
            if (!Brackets.CheckCorrectness(tokens))
                return null;

            // try to build simple one-element Boolable
            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.Bool))
                        return new BoolVariableRefer(str);
                    else
                        return null;
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
                IBoolable iboo = BuildIn(tokens);
                if (!iboo.IsNull())
                    return iboo;
            }

            // try to build LIKE function
            if (tokens.Where(t => t.GetTokenType().Equals(TokenType.Like)).Count() == 1)
            {
                IBoolable iboo = BuildLike(tokens);
                if (!iboo.IsNull())
                    return iboo;
            }

            // try to build time comparison IS AFTER/IS BEFORE
            if (ContainsOneTimeComparingToken(tokens))
            {
                IBoolable iboo = BuildTimeComparison(tokens);
                if (!iboo.IsNull())
                    return iboo;
            }

            // try to build comparison = != > < >= <=
            if (ContainsOneComparingToken(tokens) && !ContainsLogicTokens(tokens))
            {
                IBoolable iboo = BuildComparison(tokens);
                if (!iboo.IsNull())
                    return iboo;
            }

            // try to build bool function
            if (tokens.Count > 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.BracketOn)
                && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                IBoolable iboo = BoolFunction.Build(tokens);
                if (!iboo.IsNull())
                    return iboo;
            }

            // try to build expression: many elements with operators or, and, xor, not
            if (ContainsLogicTokens(tokens))
                return BuildExpression(tokens);
            else
                return null;
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

        private static bool ContainsOneTimeComparingToken(List<Token> tokens)
        {
            return tokens.Where(x => TokenGroups.IsTimeComparingSign(x.GetTokenType())).Count() == 1;
        }

        private static IBoolable BuildComparison(List<Token> tokens)
        {
            int index = tokens.TakeWhile(x => !TokenGroups.IsComparingSign(x.GetTokenType())).Count();

            if (index == 0 || index == tokens.Count - 1)
                return null;

            ComparisonType type = GetComparingToken(tokens);
            List<Token> leftTokens = tokens.GetRange(0, index);
            List<Token> rightTokens = tokens.GetRange(index + 1, tokens.Count - index - 1);
            IListable leftL = ListableBuilder.Build(leftTokens);
            IListable rightL = ListableBuilder.Build(rightTokens);

            if (leftL.IsNull()|| rightL.IsNull())
                return null;

            if (leftL is INumerable && rightL is INumerable)
                return new NumericComparison(leftL as INumerable, rightL as INumerable, type);

            if (leftL is ITimeable && rightL is ITimeable)
                return new TimeComparison(leftL as ITimeable, rightL as ITimeable, type);

            if (leftL is IStringable && rightL is IStringable)
                return new Uroboros.syntax.expressions.bools.comparisons.StringComparison(leftL as IStringable, rightL as IStringable, type);

            if (leftL is IListable && rightL is IListable)
                return new ListComparison(leftL as IListable, rightL as IListable, type);

            return null;
        }

        private static IBoolable BuildTimeComparison(List<Token> tokens)
        {
            int index = tokens.TakeWhile(x => !TokenGroups.IsTimeComparingSign(x.GetTokenType())).Count();

            if (index == 0 || index == tokens.Count - 1)
                return null;

            ComparisonType type = tokens[index].GetTokenType().Equals(TokenType.IsAfter) ? ComparisonType.Bigger : ComparisonType.Smaller;
            List<Token> leftTokens = tokens.GetRange(0, index);
            List<Token> rightTokens = tokens.GetRange(index + 1, tokens.Count - index - 1);
            ITimeable leftL = TimeableBuilder.Build(leftTokens);
            ITimeable rightL = TimeableBuilder.Build(rightTokens);

            if (leftL.IsNull() || rightL.IsNull())
                return null;

            return new TimeComparison(leftL, rightL, type);
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

            // first, merge many tokens into fewer number of IBoolables
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
                            if (!ibo.IsNull())
                                infixList.Add(ibo);
                            else
                                return null;
                            currentTokens.Clear();
                            readingFunction = false;
                            infixList.Add(new BoolExpressionOperator(GetBEOT(tok.GetTokenType())));
                        }
                        else
                            currentTokens.Add(tok);
                    }
                    else
                    {
                        if (currentTokens.Count > 0)
                        {
                            IBoolable ibo = BoolableBuilder.Build(currentTokens);
                            if (!ibo.IsNull())
                                infixList.Add(ibo);
                            else
                                return null;
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
                                if (!ibo.IsNull())
                                    infixList.Add(ibo);
                                else
                                    return null;
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
                            if (!ibo.IsNull())
                                infixList.Add(ibo);
                            else
                                return null;
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
                            if (!ibo.IsNull())
                                infixList.Add(ibo);
                            else
                                return null;
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
                if (!ibo.IsNull())
                    infixList.Add(ibo);
                else
                    return null;
            }

            // try to build negation of one boolable
            if (infixList.Count == 2 && (infixList[0] is BoolExpressionOperator) && (infixList[1] is IBoolable)
                && (infixList[0] as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not))
            {
                return new NegatedBoolable(infixList[1] as IBoolable);
            }
            
            // check if value of infixlist can be computed (check order of elements)
            if (!CheckExpressionComputability(infixList))
                throw new SyntaxErrorException("ERROR! Wrong syntax of logic expression.");

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
                    if ((previous as BoolExpressionOperator).IsNegation())
                    {
                        if (beel is BoolExpressionOperator &&
                        ((beel as BoolExpressionOperator).IsNegation() || 
                         (beel as BoolExpressionOperator).IsBracketOff() || 
                         (beel as BoolExpressionOperator).IsBinaryOperator()))
                                return false;
                    }
                    else if ((previous as BoolExpressionOperator).IsBracketOn())
                    {
                        if (beel is BoolExpressionOperator &&
                            (beel as BoolExpressionOperator).IsBinaryOperator())
                            return false;
                    }
                    else if ((previous as BoolExpressionOperator).IsBracketOff())
                    {
                        if ((beel is BoolExpressionOperator &&
                        ((beel as BoolExpressionOperator).IsNegation() ||
                         (beel as BoolExpressionOperator).IsBracketOn())) ||
                          beel is IBoolable)
                            return false;
                    }
                    else if ((previous as BoolExpressionOperator).IsBinaryOperator())
                    {
                        if (beel is BoolExpressionOperator &&
                            ((beel as BoolExpressionOperator).IsBracketOff() || 
                             (beel as BoolExpressionOperator).IsBinaryOperator()))
                            return false;
                    }
                }

                if (previous is IBoolable)
                {
                    if ((beel is BoolExpressionOperator &&
                        ((beel as BoolExpressionOperator).IsNegation() ||
                         (beel as BoolExpressionOperator).IsBracketOn()))||
                          beel is IBoolable)
                        return false;
                }
                previous = beel;
            }

            if (infixList.First() is BoolExpressionOperator &&
                        ((infixList.First() as BoolExpressionOperator).IsBracketOff() ||
                         (infixList.First() as BoolExpressionOperator).IsBinaryOperator()))
                return false;

            if (infixList.Last() is BoolExpressionOperator &&
                        ((infixList.Last() as BoolExpressionOperator).IsNegation() ||
                         (infixList.Last() as BoolExpressionOperator).IsBracketOn() ||
                         (infixList.Last() as BoolExpressionOperator).IsBinaryOperator()))
                return false;

            return true;
        }

        private static List<IBoolExpressionElement> ReversePolishNotation(List<IBoolExpressionElement> infixList)
        {
            // Dijkstra algo of convertion to Reverse Polish Notation

            Stack<BoolExpressionOperator> operatorStack = new Stack<BoolExpressionOperator>();
            List<IBoolExpressionElement> output = new List<IBoolExpressionElement>();

            // reverse order of elements so can be read left-to-right
            // includes reversing brackets
            infixList.Reverse();
            foreach (IBoolExpressionElement bee in infixList)
            {
                if (bee is BoolExpressionOperator)
                    (bee as BoolExpressionOperator).ReverseBracket();
            }

            foreach (IBoolExpressionElement ibee in infixList)
            {
                if (ibee is IBoolable)
                    output.Add(ibee);

                if (ibee is BoolExpressionOperator)
                {
                    if ((ibee as BoolExpressionOperator).IsBracketOff())
                    {
                        while (operatorStack.Count > 0)
                        {
                            BoolExpressionOperator beo = operatorStack.Pop();

                            if (beo.IsBracketOn())
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
            return ComparisonType.Equals;
        }

        public static IBoolable BuildIn(List<Token> tokens)
        {
            int index = tokens.TakeWhile(x => !x.GetTokenType().Equals(TokenType.In)).Count();
            if (index == 0 || index == tokens.Count - 1)
                return null;

            List<Token> leftTokens = tokens.GetRange(0, index);
            List<Token> rightTokens = tokens.GetRange(index + 1, tokens.Count - index - 1);

            IStringable istr = StringableBuilder.Build(leftTokens);
            if (istr.IsNull())
                return null;

            IListable ilis = ListableBuilder.Build(rightTokens);
            if (ilis.IsNull())
                return null;

            return new In(istr, ilis);
        }

        public static IBoolable BuildLike(List<Token> tokens)
        {
            int index = tokens.TakeWhile(x => !x.GetTokenType().Equals(TokenType.Like)).Count();
            if (index == 0 || index == tokens.Count - 1)
                return null;

            List<Token> leftTokens = tokens.GetRange(0, index);
            List<Token> rightTokens = tokens.GetRange(index + 1, tokens.Count - index - 1);

            IStringable istr = StringableBuilder.Build(leftTokens);
            if (istr.IsNull())
                return null;

            if (rightTokens.Count == 1 && rightTokens[0].GetTokenType().Equals(TokenType.StringConstant))
            {
                string phrase = rightTokens[0].GetContent();
                CheckLikePhraseCorrectness(phrase);
                return new Like(istr, phrase);
            }
            else
                return null;
        }

        private static void CheckLikePhraseCorrectness(string phrase)
        {
            if (phrase.Length == 0)
                throw new SyntaxErrorException("ERROR! Expression 'like' has empty comparing phrase.");

            if (phrase.Length == 1)
                return;

            for (int i = 1; i < phrase.Length; i++)
            {
                if (phrase[i - 1] == '%' && (phrase[i] == '%' || phrase[i] == '_'))
                    throw new SyntaxErrorException("ERROR! Expression \"like " + phrase + "\" is not correct.");
            }
            return;
        }
    }
}
