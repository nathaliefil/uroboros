using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.expressions.list.subcommands.orderby;
using Uroboros.syntax.variables;

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

            if (type.Equals(TokenType.With))
                return BuildWith(tokens, false);

            if (type.Equals(TokenType.Without))
                return BuildWith(tokens, true);

            if (type.Equals(TokenType.Where))
                return BuildWhere(tokens);

            if (type.Equals(TokenType.OrderBy))
                return BuildOrderBy(tokens);

            throw new SyntaxErrorException("ERROR! Unknown keyword in list declaration."); // this is never thrown
        }

        public static ISubcommand BuildEmpty(TokenType type)
        {
            if (type == TokenType.First || type == TokenType.Last || type == TokenType.Skip)
                return new NumericSubcommand(new NumericConstant(1), GetNumericType(type));

            throw new SyntaxErrorException("ERROR! Subcommand " + GetName(type) + " is empty.");
        }

        public static ISubcommand BuildWith(List<Token> tokens, bool negated)
        {
            string name = negated ? "without" : "with";

            IListable ilis = ListableBuilder.Build(tokens);
            if (ilis.IsNull())
                throw new SyntaxErrorException("ERROR! In list declaration there is something wrong with expression: " + name + ".");
            else
                return new With(ilis, negated);
        }

        public static ISubcommand BuildNumeric(List<Token> tokens, TokenType type)
        {
            INumerable inu = NumerableBuilder.Build(tokens);
            if (inu.IsNull())
                throw new SyntaxErrorException("ERROR! In list declaration there is something wrong with expression: " + GetName(type) + ".");
            else
                return new NumericSubcommand(inu, GetNumericType(type));
        }

        public static ISubcommand BuildWhere(List<Token> tokens)
        {
            IBoolable iboo = BoolableBuilder.Build(tokens);
            if (iboo.IsNull())
                throw new SyntaxErrorException("ERROR! In list declaration there is something wrong with expression: where.");
            else
                return new Where(iboo);
        }

        public static ISubcommand BuildOrderBy(List<Token> tokens)
        {
            List<OrderByStruct> variables = new List<OrderByStruct>();
            OrderByStruct waitingVariable = null;
            bool expectedVariable = true;

            foreach (Token tok in tokens)
            {
                if (!tok.GetTokenType().Equals(TokenType.Variable))
                    throw new SyntaxErrorException("ERROR! Expression 'order by' contains not allowed words or characters.");

                if (expectedVariable)
                {
                    OrderByStruct obv = BuildOrderByStruct(tok);
                    if (obv.IsNull())
                        if (variables.Count == 0)
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
                        OrderByStruct obv = BuildOrderByStruct(tok);
                        if (obv.IsNull())
                            throw new SyntaxErrorException("ERROR! Expression 'order by' contains not allowed variable " + tok.GetContent() + ".");
                        else
                        {
                            if (waitingVariable.Equals(OrderByVariable.None))
                                waitingVariable = obv;
                            else
                            {
                                variables.Add(waitingVariable);
                                waitingVariable = obv;
                            }
                        }
                    }
                    else
                    {
                        if (obt.Equals(OrderByType.DESC))
                            waitingVariable.SetDesc();
                        variables.Add(waitingVariable);
                        expectedVariable = true;
                        waitingVariable = null;
                    }
                }
            }
            if (!waitingVariable.IsNull())
                variables.Add(waitingVariable);

            if (variables.Count == 0)
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

        private static OrderByStruct BuildOrderByStruct(Token tok)
        {
            switch (tok.GetContent().ToLower())
            {
                case "creation":
                    return new OrderByStruct(OrderByVariable.Creation);
                case "extension":
                    return new OrderByStruct(OrderByVariable.Extension);
                case "fullname":
                    return new OrderByStruct(OrderByVariable.Fullname);
                case "modification":
                    return new OrderByStruct(OrderByVariable.Modification);
                case "name":
                    return new OrderByStruct(OrderByVariable.Name);
                case "size":
                    return new OrderByStruct(OrderByVariable.Size);
            }

            // struct - element of time variable (creation/modification)
            string name = tok.GetContent();
            int count = name.Count(c => c == '.');

            if (count != 1)
                return null;

            string leftSide = name.Substring(0, name.IndexOf('.')).ToLower();
            string rightSide = name.Substring(name.IndexOf('.') + 1).ToLower();

            if (leftSide.Length == 0 || rightSide.Length == 0)
                return null;

            OrderByVariable obv;

            if (leftSide.ToLower().Equals("creation"))
                obv = OrderByVariable.Creation;
            else if (leftSide.ToLower().Equals("modification"))
                obv = OrderByVariable.Modification;
            else
                return null;

            switch (rightSide.ToLower())
            {
                case "year":
                    return new OrderByStructTime(obv, TimeVariableType.Year);
                case "month":
                    return new OrderByStructTime(obv, TimeVariableType.Month);
                case "day":
                    return new OrderByStructTime(obv, TimeVariableType.Day);
                case "weekday":
                    return new OrderByStructTime(obv, TimeVariableType.WeekDay);
                case "hour":
                    return new OrderByStructTime(obv, TimeVariableType.Hour);
                case "minute":
                    return new OrderByStructTime(obv, TimeVariableType.Minute);
                case "second":
                    return new OrderByStructTime(obv, TimeVariableType.Second);
                case "date":
                    return new OrderByStructDate(obv);
                case "clock":
                    return new OrderByStructClock(obv);
            }
            return null;
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
