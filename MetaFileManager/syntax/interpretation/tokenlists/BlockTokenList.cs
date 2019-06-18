using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.commands;
using DivineScript.syntax.runtime;
using DivineScript.syntax.commands.blocks;
using DivineScript.syntax.interpretation.expressions;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.interpretation.vars_range;

namespace DivineScript.syntax.interpretation.tokenlists
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
            List<ICommand> block = new List<ICommand>();
            List<ICommand> commands = new List<ICommand>();

            InterVariables.GetInstance().BracketsUp();
            foreach (TokenList tl in elements)
            {
                commands.AddRange(tl.ToCommands());
            }
            InterVariables.GetInstance().BracketsDown();

            if (!precedings)
            {
                block.Add(new Block(commands));
                return block;
            }
            else
            {
                switch (precedingTokens[0].GetTokenType())
                {
                    case TokenType.If:
                    {
                        precedingTokens.RemoveAt(0);
                        if (precedingTokens.Count == 0)
                            throw new SyntaxErrorException("ERROR! IF statement is empty.");
                        
                        IBoolable iboo = BoolableBuilder.Build(precedingTokens);
                        if (iboo is NullVariable)
                            throw new SyntaxErrorException("ERROR! There are is something wrong with condition in IF statement.");
                        block.Add(new IfBlock(commands, iboo));
                        return block;
                    }
                    case TokenType.While:
                    {
                        precedingTokens.RemoveAt(0);
                        if (precedingTokens.Count == 0)
                            throw new SyntaxErrorException("ERROR! WHILE statement is empty.");

                        IBoolable iboo = BoolableBuilder.Build(precedingTokens);
                        if (iboo is NullVariable)
                            throw new SyntaxErrorException("ERROR! There are is something wrong with condition in WHILE statement.");
                        block.Add(new WhileBlock(commands, iboo));
                        return block;
                    }
                }
                INumerable inum = NumerableBuilder.Build(precedingTokens);
                if (inum is NullVariable)
                {
                    IListable ilist = ListableBuilder.Build(precedingTokens);
                    if (ilist is NullVariable)
                        throw new SyntaxErrorException("ERROR! There are is something wrong with code preceding block of instructions.");
                    block.Add(new ListBlock(commands, ilist));
                }
                else
                {
                    block.Add(new RepeatBlock(commands, inum));
                }
                return block;
            }
        }
    }
}
