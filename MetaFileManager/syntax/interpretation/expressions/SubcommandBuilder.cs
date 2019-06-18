using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.expressions.list.subcommands;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.interpretation.expressions
{
    class SubcommandBuilder
    {
        public static ISubcommand Build(List<Token> tokens, TokenType type)
        {
            if (type.Equals(TokenType.First) || type.Equals(TokenType.Last)
                || type.Equals(TokenType.Each) || type.Equals(TokenType.Skip))
            {
                return BuildNumeric(tokens, type);
            }

            if (type.Equals(TokenType.Where))
                return BuildWhere(tokens);

            if (type.Equals(TokenType.OrderBy))
                return BuildOrderBy(tokens);

            throw new SyntaxErrorException("ERROR! Unknown keyword in list declaration"); // this is never thrown
        }

        public static ISubcommand BuildNumeric(List<Token> tokens, TokenType type)
        {
            INumerable inu = NumerableBuilder.Build(tokens);
            if (inu is NullVariable)
                throw new SyntaxErrorException("ERROR! In list declaration there is something wrong with expression: " + GetName(type) + ".");
            else
                return new NumericSubcommand(inu, GetNumericType(type));
        }

        public static ISubcommand BuildWhere(List<Token> tokens)
        {
            IBoolable iboo = BoolableBuilder.Build(tokens);
            if (iboo is NullVariable)
                throw new SyntaxErrorException("ERROR! In list declaration there is something wrong with expression: where.");
            else
                return new Where(iboo);
        }

        public static ISubcommand BuildOrderBy(List<Token> tokens)
        {

            // todo


            throw new SyntaxErrorException("ERROR! Order by is not implemented yet!");
        }

        private static string GetName(TokenType type)
        {
            switch (type)
            {
                case TokenType.First:
                    return "first";
                case TokenType.Last:
                    return "last";
                case TokenType.Skip:
                    return "skip";
                case TokenType.Each:
                    return "each";
            }
            return "";
        }

        private static NumericSubcommandType GetNumericType(TokenType type)
        {
            switch (type)
            {
                case TokenType.First:
                    return NumericSubcommandType.First;
                case TokenType.Last:
                    return NumericSubcommandType.Last;
                case TokenType.Skip:
                    return NumericSubcommandType.Skip;
                case TokenType.Each:
                    return NumericSubcommandType.Each;
            }
            return NumericSubcommandType.First;
        }

    }
}



/*
            TokenType.Where, TokenType.First, 
            TokenType.Last, TokenType.Skip,  TokenType.Each, TokenType.OrderBy
            */