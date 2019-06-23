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

namespace Uroboros.syntax.interpretation.expressions
{
    class BoolableBuilder
    {
        public static IBoolable Build(List<Token> tokens)
        {
            Token wwtok = TokenGroups.WrongTokenInExpression(tokens);
            if (!wwtok.GetTokenType().Equals(TokenType.Null))
                return new NullVariable();

            Brackets.CheckCorrectness(tokens, false);

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

            if (ContainsOneComparingToken(tokens))
            {
                IBoolable iboo = BuildComparison(tokens);
                if (!(iboo is NullVariable))
                    return iboo;
            }

            if (tokens.Where(x => TokenGroups.IsLogicSign(x.GetTokenType())).Any())
                return BuildExpression(tokens);
            else
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
            // here we go
            /// todo

            return new NullVariable();
        }

        private static bool ContainsOneComparingToken(List<Token> tokens)
        {
            int count = tokens.Where(x => TokenGroups.IsComparingSign(x.GetTokenType())).Count();
            return count == 1 ? true : false;
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
