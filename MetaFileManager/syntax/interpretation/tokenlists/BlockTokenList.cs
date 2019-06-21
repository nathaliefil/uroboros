using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.commands;
using Uroboros.syntax.runtime;
using Uroboros.syntax.commands.blocks;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.vars_range;

namespace Uroboros.syntax.interpretation.tokenlists
{
    class BlockTokenList : TokenList
    {
        private List<Token> precedingTokens;
        private List<TokenList> elements;
        private bool precedings;

        public BlockTokenList(List<Token> precedingTokens, List<Token> tokens)
            : base(tokens)
        {
            this.precedingTokens = precedingTokens;
            precedings = true;
            BuildItself(tokens);
        }

        public BlockTokenList(List<Token> tokens)
            : base(tokens)
        {
            precedings = false;
            BuildItself(tokens);
        }

        private void BuildItself(List<Token> tokens)
        {
            elements = new List<TokenList>();

            bool fillingBlock = false;
            int level = 0;
            List<Token> pre = new List<Token>();
            List<Token> got = new List<Token>();

            foreach (Token tok in tokens)
            {
                if (!fillingBlock)
                {
                    if (!tok.GetTokenType().Equals(TokenType.CurlyBracketOn))
                    {
                        got.Add(tok);
                    }
                    else
                    {
                        fillingBlock = true;
                        level++;
                        int position;

                        for (position = got.Count - 1; position >= 0; position--)
                        {
                            if (got[position].GetTokenType().Equals(TokenType.CurlyBracketOff)
                                || got[position].GetTokenType().Equals(TokenType.Semicolon))
                            {
                                break;
                            }
                        }
                        pre = new List<Token>();
                        List<Token> lis = new List<Token>();

                        for (int i = position + 1; i < got.Count; i++)
                        {
                            pre.Add(got[i].Clone());
                        }

                        for (int i = 0; i < position; i++)
                        {
                            lis.Add(got[i].Clone());
                        }
                        if (lis.Count > 0)
                        {
                            elements.Add(new TokenList(lis));
                        }
                        got.Clear();
                    }
                }
                else
                {
                    if (tok.GetTokenType().Equals(TokenType.CurlyBracketOn))
                    {
                        level++;
                    }
                    if (tok.GetTokenType().Equals(TokenType.CurlyBracketOff))
                    {
                        level--;
                    }

                    if (level > 0)
                    {
                        got.Add(tok);
                    }
                    else
                    {
                        fillingBlock = false;
                        if (got.Count > 0)
                        {
                            if (pre.Count > 0)
                            {
                                elements.Add(new BlockTokenList(pre.Select(t => (Token)t.Clone()).ToList(),
                                    got.Select(t => (Token)t.Clone()).ToList())); // create deep copy 'just in case'
                                pre.Clear();
                            }
                            else
                            {
                                elements.Add(new BlockTokenList(got.Select(t => (Token)t.Clone()).ToList()));
                            }
                            got.Clear();
                        }
                    }
                }
            }
            if (got.Count > 0)
            {
                elements.Add(new TokenList(got));
            }
        }

        public override List<ICommand> ToCommands()
        {
            List<ICommand> commands = new List<ICommand>();

            InterVariables.GetInstance().BracketsUp();
            foreach (TokenList tl in elements)
                commands.AddRange(tl.ToCommands());

            InterVariables.GetInstance().BracketsDown();

            if (!precedings)
                return new List<ICommand>{ new Block(commands)};
            else
            {
                switch (precedingTokens[0].GetTokenType())
                {
                    case TokenType.If:
                        return new List<ICommand>{ BuildIfBlock(precedingTokens, commands)};
                    case TokenType.While:
                        return new List<ICommand>{ BuildWhileBlock(precedingTokens, commands)};
                }
                return new List<ICommand>{ BuildRepeatingBlock(precedingTokens, commands)};
            }
        }
    }
}
