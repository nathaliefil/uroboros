using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.list.subcommands;
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
            List<OrderByStruct> variables = new List<OrderByStruct>();
            OrderByVariable waitingVariable = OrderByVariable.None;
            bool expectedVariable = true;

            foreach (Token tok in tokens)
            {
                if (!tok.GetTokenType().Equals(TokenType.Variable))
                    throw new SyntaxErrorException("ERROR! Expression 'order by' contains not allowed words or characters.");

                if (expectedVariable)
                {
                    OrderByVariable obv = BuildOrderByVariable(tok);
                    if (obv.Equals(OrderByVariable.None))
                        if(variables.Count == 0)
                            throw new SyntaxErrorException("ERROR! Expression 'order by' do not start with allowed variable.");
                        else
                            throw new SyntaxErrorException("ERROR! Expression 'order by' contains adjacent keywords asc/desc or one not allowed variable.");
                    else
                        waitingVariable = obv;
                        expectedVariable = false;
                }
                else
                {
                    OrderByType obt = BuildOrderByType(tok);
                    if (obt.Equals(OrderByType.None))
                    {
                        OrderByVariable obv = BuildOrderByVariable(tok);
                        if (obv.Equals(OrderByVariable.None))
                            throw new SyntaxErrorException("ERROR! Expression 'order by' contains not allowed variable " + tok.GetContent() + ".");
                        else
                        {
                            if (waitingVariable.Equals(OrderByVariable.None))
                                waitingVariable = obv;
                            else
                            {
                                variables.Add(new OrderByStruct(waitingVariable, OrderByType.ASC));
                                waitingVariable = obv;
                            }
                        }
                    }
                    else
                    {
                        variables.Add(new OrderByStruct(waitingVariable, obt));
                        expectedVariable = true;
                        waitingVariable = OrderByVariable.None;
                    }
                }
            }
            if (!waitingVariable.Equals(OrderByVariable.None))
                variables.Add(new OrderByStruct(waitingVariable, OrderByType.ASC));

            if(variables.Count == 0)
                throw new SyntaxErrorException("ERROR! Expression 'order by' is empty.");

            return new OrderBy(variables);
        }

        private static OrderByType BuildOrderByType(Token tok)
        {
            switch (tok.GetContent().ToLower())
            {
                case "asc":
                    return OrderByType.ASC;
                case "desc":
                    return OrderByType.DESC;
            }
            return OrderByType.None;
        }

        private static OrderByVariable BuildOrderByVariable(Token tok)
        {
            switch (tok.GetContent().ToLower())
            {
                case "creation":
                    return OrderByVariable.Creation;
                case "extension":
                    return OrderByVariable.Extension;
                case "fullname":
                    return OrderByVariable.Fullname;
                case "modification":
                    return OrderByVariable.Modification;
                case "name":
                    return OrderByVariable.Name;
                case "size":
                    return OrderByVariable.Size;
            }
            return OrderByVariable.None;
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
