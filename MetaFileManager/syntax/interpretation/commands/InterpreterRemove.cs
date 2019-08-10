using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.commands.list;
using Uroboros.syntax.variables.refers;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterRemove
    {
        public static ICommand Build(List<Token> tokens)
        {
            TokenType type = tokens[0].GetTokenType();
            tokens.RemoveAt(0);

            if (!tokens.Any(t => t.GetTokenType().Equals(TokenType.From)))
                throw new SyntaxErrorException("ERROR! Command 'remove' do not contain keyword 'from'.");
            if (tokens.Where(t => t.GetTokenType().Equals(TokenType.From)).Count() > 1)
                throw new SyntaxErrorException("ERROR! In command 'remove' keyword 'from' occurs too many times.");

            List<Token> part1 = new List<Token>();
            List<Token> part2 = new List<Token>();
            bool pastTo = false;
            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.From))
                    pastTo = true;
                else
                {
                    if (pastTo)
                        part2.Add(tok);
                    else
                        part1.Add(tok);
                }
            }

            if (part2.Count == 0)
                throw new SyntaxErrorException("ERROR! Command 'remove' do not contain definition for target variable.");
            if (part2.Count > 2 || !part2[0].GetTokenType().Equals(TokenType.Variable))
                throw new SyntaxErrorException("ERROR! Target variable in command 'remove' cannot be read.");

            string name = part2[0].GetContent();

            if (!InterVariables.GetInstance().ContainsChangable(name, InterVarType.List))
                throw new SyntaxErrorException("ERROR! In command 'remove' variable " + name + " do not exist or cannot be read as list.");

            // add this
            if (part1.Count == 0)
                return new Remove(name, new StringVariableRefer("this") as IStringable);

            IListable ilist = ListableBuilder.Build(part1);
            if (ilist.IsNull())
                throw new SyntaxErrorException("ERROR! In command 'remove' definition for elements to add cannot be read as list.");

            if (ilist is IStringable)
                return new Remove(name, ilist as IStringable);
            else
                return new Remove(name, ilist);
        }
    }
}
