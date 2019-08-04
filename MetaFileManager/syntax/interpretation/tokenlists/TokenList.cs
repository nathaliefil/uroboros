using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.commands;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.commands.blocks;

namespace Uroboros.syntax.interpretation.tokenlists
{
    class TokenList
    {
        private List<Token> tokens;

        public TokenList(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public virtual List<ICommand> ToCommands()
        {
            return BuildSingleCommands();
        }

        protected List<ICommand> BuildSingleCommands()
        {
            List<ICommand> commands = new List<ICommand>();
            List<Token> currentTokens = new List<Token>();

            for (int i = 0; i < tokens.Count; i++)
            {
                if (!tokens[i].GetTokenType().Equals(TokenType.Semicolon))
                    currentTokens.Add(tokens[i].Clone());
                else
                {
                    if (currentTokens.Count > 0)
                        commands.Add( BuildSingleCommandIncludingArrowFunction(currentTokens));
                    currentTokens.Clear();
                }
            }
            if (currentTokens.Count > 0)
                commands.Add( BuildSingleCommandIncludingArrowFunction(currentTokens));
            return commands;
        }

        protected ICommand BuildSingleCommandIncludingArrowFunction(List<Token> tokens)
        {
            if (!tokens.Any(t => t.GetTokenType().Equals(TokenType.BigArrow)))
                return SingleCommandFactory.Build(tokens);
            else
            {
                if (tokens.Where(t => t.GetTokenType().Equals(TokenType.BigArrow)).Count() > 1)
                    throw new SyntaxErrorException("ERROR! In one command multiple arrow functions detected.");
                else
                {
                    List<Token> part1 = new List<Token>();
                    List<Token> part2 = new List<Token>();
                    bool pastArrow = false;
                    foreach (Token tok in tokens)
                    {
                        if (tok.GetTokenType().Equals(TokenType.BigArrow))
                            pastArrow = true;
                        else
                        {
                            if (pastArrow)
                                part2.Add(tok);
                            else
                                part1.Add(tok);
                        }
                    }
                    if (part1.Count == 0)
                        throw new SyntaxErrorException("ERROR! Left side of arrow function is empty.");
                    if (part2.Count == 0)
                        throw new SyntaxErrorException("ERROR! Right side of arrow function is empty.");

                    ICommand cmnd = SingleCommandFactory.Build(part2);

                    if(part1[0].GetTokenType().Equals(TokenType.If))
                        return BuildIfBlock(part1, new List<ICommand> {cmnd});
                    if (part1[0].GetTokenType().Equals(TokenType.While))
                        return BuildWhileBlock(part1, new List<ICommand> { cmnd });

                    return BuildRepeatingBlock(part1, new List<ICommand> { cmnd });
                }
            }
        }

        protected ICommand BuildIfBlock(List<Token> precedings, List<ICommand> insides)
        {
            precedings.RemoveAt(0);
            if (precedings.Count == 0)
                throw new SyntaxErrorException("ERROR! IF statement is empty.");

            IBoolable iboo = BoolableBuilder.Build(precedings);
            if (iboo.IsNull())
                throw new SyntaxErrorException("ERROR! There are is something wrong with condition in IF statement.");
            return new IfBlock(insides, iboo);
        }

        protected ICommand BuildWhileBlock(List<Token> precedings, List<ICommand> insides)
        {
            precedings.RemoveAt(0);
            if (precedings.Count == 0)
                throw new SyntaxErrorException("ERROR! WHILE statement is empty.");

            IBoolable iboo = BoolableBuilder.Build(precedings);
            if (iboo.IsNull())
                throw new SyntaxErrorException("ERROR! There are is something wrong with condition in WHILE statement.");
            return new WhileBlock(insides, iboo);
        }

        protected ICommand BuildRepeatingBlock(List<Token> precedings, List<ICommand> insides)
        {
            INumerable inum = NumerableBuilder.Build(precedings);
            if (inum.IsNull())
            {
                IListable ilist = ListableBuilder.Build(precedings);
                if (ilist.IsNull())
                    throw new SyntaxErrorException("ERROR! There are is something wrong with code preceding block of instructions.");
                return new ListBlock(insides, ilist);
            }
            else
                return new RepeatBlock(insides, inum);

        }
    }
}
