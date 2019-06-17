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
                        ///todo
                        ///this not works
                        ///
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
                        }
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
