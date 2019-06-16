using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.reading;
using DivineScript.syntax.commands;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.interpretation.tokenlists
{
    class BlockTokenList : ITokenList
    {
        List<Token> precedingTokens;
        List<ITokenList> elements;
        bool precedings;

        public BlockTokenList(List<Token> precedingTokens, List<Token> tokens)
        {
            this.precedingTokens = precedingTokens;
            precedings = true;
            BuildItself(tokens);
        }

        public BlockTokenList(List<Token> tokens)
        {
            precedings = false;
            BuildItself(tokens);
        }

        private void BuildItself(List<Token> tokens)
        {
            elements = new List<ITokenList>();

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

                        List<Token> got2 = got.Select(t => (Token)t.Clone()).ToList();
                        got2.Reverse();
                        got.Clear();

                        foreach (Token tk in got)
                        {
                            if (tk.GetTokenType().Equals(TokenType.CurlyBracketOff)
                                || tk.GetTokenType().Equals(TokenType.Semicolon))
                            {
                                break;
                            }
                            else
                            {
                                pre.Add(tk);
                                got2.Remove(tk);
                            }
                        }
                        pre.Reverse();
                        got2.Reverse();
                        if (got2.Count > 0)
                        {
                            elements.Add(new TokenList(got2));
                        }
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
                            elements.Add(new BlockTokenList(pre, got));
                        }
                        pre.Clear();
                        got.Clear();
                    }
                }
            }
            if (got.Count > 0)
            {
                elements.Add(new TokenList(got));
            }

            Logger.GetInstance().Log(" "+elements.Count);
        }

        public List<ICommand> ToCommands()
        {
            List<ICommand> commands = new List<ICommand>();

            // code

            return commands;
        }

    }
}
