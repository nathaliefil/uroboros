﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.lexer;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.expressions.bools;

namespace Uroboros.syntax.interpretation.expressions
{
    class TernaryBuilder
    {
        public static bool IsPossibleTernary(List<Token> tokens)
        {
            int questionIndex = TokenGroups.IndexOfTokenOutsideBrackets(tokens, TokenType.QuestionMark);
            int colonIndex = TokenGroups.IndexOfTokenOutsideBrackets(tokens, TokenType.Colon);

            // there is no question mark / colon
            if (questionIndex == -1 || colonIndex == -1)
                return false;

            // question mark is after colon
            if (questionIndex > colonIndex)
                return false;

            // colon comes right after question mark
            if (colonIndex == questionIndex + 1)
                return false;

            // expression starts with question mark or ends with colon
            if (questionIndex == 0 || colonIndex == tokens.Count - 1)
                return false;

            return true;
        }

        private static List<Token> GetTernaryCondition(List<Token> tokens)
        {
            int questionIndex = TokenGroups.IndexOfTokenOutsideBrackets(tokens, TokenType.QuestionMark);
            return tokens.Take(questionIndex).ToList();
        }

        private static List<Token> GetTernaryConfirmation(List<Token> tokens)
        {
            int questionIndex = TokenGroups.IndexOfTokenOutsideBrackets(tokens, TokenType.QuestionMark);
            int colonIndex = TokenGroups.IndexOfTokenOutsideBrackets(tokens, TokenType.Colon);
            return tokens.GetRange(questionIndex + 1, colonIndex - questionIndex - 1);
        }

        private static List<Token> GetTernaryNegation(List<Token> tokens)
        {
            int colonIndex = TokenGroups.IndexOfTokenOutsideBrackets(tokens, TokenType.Colon);
            return tokens.Skip(colonIndex + 1).ToList();
        }



        // builders start here

        public static IBoolable BuildBoolTernary(List<Token> tokens)
        {
            IBoolable condition = BoolableBuilder.Build(GetTernaryCondition(tokens));
            if (condition.IsNull())
                return null;

            IBoolable confirmationCase = BoolableBuilder.Build(GetTernaryConfirmation(tokens));
            if (confirmationCase.IsNull())
                return null;

            IBoolable negationCase = BoolableBuilder.Build(GetTernaryNegation(tokens));
            if (negationCase.IsNull())
                return null;

            return new BoolTernary(condition, confirmationCase, negationCase);
        }

        public static INumerable BuildNumericTernary(List<Token> tokens)
        {
            IBoolable condition = BoolableBuilder.Build(GetTernaryCondition(tokens));
            if (condition.IsNull())
                return null;

            INumerable confirmationCase = NumerableBuilder.Build(GetTernaryConfirmation(tokens));
            if (confirmationCase.IsNull())
                return null;

            INumerable negationCase = NumerableBuilder.Build(GetTernaryNegation(tokens));
            if (negationCase.IsNull())
                return null;

            return new NumericTernary(condition, confirmationCase, negationCase);
        }

        public static ITimeable BuildTimeTernary(List<Token> tokens)
        {
            IBoolable condition = BoolableBuilder.Build(GetTernaryCondition(tokens));
            if (condition.IsNull())
                return null;

            ITimeable confirmationCase = TimeableBuilder.Build(GetTernaryConfirmation(tokens));
            if (confirmationCase.IsNull())
                return null;

            ITimeable negationCase = TimeableBuilder.Build(GetTernaryNegation(tokens));
            if (negationCase.IsNull())
                return null;

            return new TimeTernary(condition, confirmationCase, negationCase);
        }

        public static IStringable BuildStringTernary(List<Token> tokens)
        {
            IBoolable condition = BoolableBuilder.Build(GetTernaryCondition(tokens));
            if (condition.IsNull())
                return null;

            IStringable confirmationCase = StringableBuilder.Build(GetTernaryConfirmation(tokens));
            if (confirmationCase.IsNull())
                return null;

            IStringable negationCase = StringableBuilder.Build(GetTernaryNegation(tokens));
            if (negationCase.IsNull())
                return null;

            return new StringTernary(condition, confirmationCase, negationCase);
        }

        public static IListable BuildListTernary(List<Token> tokens)
        {
            IBoolable condition = BoolableBuilder.Build(GetTernaryCondition(tokens));
            if (condition.IsNull())
                return null;

            IListable confirmationCase = ListableBuilder.Build(GetTernaryConfirmation(tokens));
            if (confirmationCase.IsNull())
                return null;

            IListable negationCase = ListableBuilder.Build(GetTernaryNegation(tokens));
            if (negationCase.IsNull())
                return null;

            return new ListTernary(condition, confirmationCase, negationCase);
        }
    }
}
