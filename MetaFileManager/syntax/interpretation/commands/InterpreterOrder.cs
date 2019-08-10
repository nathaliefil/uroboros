using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.commands.list;
using Uroboros.syntax.expressions.list.subcommands.orderby;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterOrder
    {
        public static ICommand Build(List<Token> tokens)
        {
            TokenType type = tokens[0].GetTokenType();
            tokens.RemoveAt(0);

            if (tokens.Count < 3)
                throw new SyntaxErrorException("ERROR! Order command is too short.");
            if (tokens.Where(t => t.GetTokenType().Equals(TokenType.By)).Count() > 1)
                throw new SyntaxErrorException("ERROR! In order command keyword 'by' occurs too many times.");
            if (!tokens[0].GetTokenType().Equals(TokenType.Variable))
                throw new SyntaxErrorException("ERROR! Second word of order command must be name of list to order.");
            if (!tokens[1].GetTokenType().Equals(TokenType.By))
                throw new SyntaxErrorException("ERROR! In order command name of list contains spaces or other unallowed symbols.");

            string name = tokens[0].GetContent();

            if (!InterVariables.GetInstance().ContainsChangable(name, InterVarType.String) && InterVariables.GetInstance().ContainsChangable(name, InterVarType.List))
            {
                tokens.RemoveAt(0);
                tokens.RemoveAt(0);

                ISubcommand ord = SubcommandBuilder.Build(tokens, TokenType.OrderBy);
                if (ord is OrderBy)
                    return new Order(name, ord as OrderBy);
                else
                    throw new SyntaxErrorException("ERROR! In order command there is something wrong with variables.");
            }
            else
                throw new SyntaxErrorException("ERROR! In order command variable " + name + " do not exist, cannnot be read as list or cannot be modified.");
        }
    }
}
