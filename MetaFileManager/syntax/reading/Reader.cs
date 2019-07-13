using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax;

namespace Uroboros.syntax.reading
{
    class Reader
    {
        private static char[] KEY_SIGNS = new char[] 
                {',', '!', '=', '(', ')', '{', '}', '[', ']',';', ':',
                 '-', '+', '*', '"', '%', '/', '&', '|', '^', '<', '>'};


        public static List<Token> CreateTokenlist(string code)
        {
            List<Token> tokens = new List<Token>();
            StringBuilder stringb = new StringBuilder();

            char previousChar = '\0';
            TokenType previousToken = TokenType.Null;

            bool commentSingleOn = false;
            bool commentMultilineOn = false;
            bool quotationOn = false;

            foreach (char ch in code)
            {
                if (quotationOn)
                {
                    if (ch == '"')
                    {
                        Token tok = TokenFactory.BuildQuotationToken(stringb.ToString());
                        tokens.Add(tok);
                        previousToken = tok.GetTokenType();
                        stringb.Clear();
                        quotationOn = false;
                    }
                    else
                    {
                        stringb.Append(ch);
                    }
                }
                else
                {
                    if (commentSingleOn)
                    {
                        if (ch == '\n' || ch=='\t' || ch=='\r' || ch.Equals("\r\n"))
                        {
                            commentSingleOn = false;
                        }
                    }
                    else
                    {
                        if (commentMultilineOn)
                        {
                            if (previousChar == '*' && ch == '/')
                            {
                                commentMultilineOn = false;
                            }
                            //escape from multiline
                        }
                        else
                        {
                            //everything
                            bool done = false;

                            if (ch == '"')
                            {
                                done = true;
                                if (stringb.Length > 0)
                                {
                                    Token tok = TokenFactory.Build(stringb.ToString());
                                    tokens.Add(tok);
                                    previousToken = tok.GetTokenType();
                                    stringb.Clear();
                                }
                                quotationOn = true;
                            }

                            if (ch == ' ' || ch == '\n' || ch == '\t' || ch == '\r' || ch.Equals("\r\n"))
                            {
                                done = true;
                                if (stringb.Length > 0)
                                {
                                    Token tok = TokenFactory.Build(stringb.ToString());
                                    tokens.Add(tok);
                                    previousToken = tok.GetTokenType();
                                    stringb.Clear();
                                }
                            }

                            if (KEY_SIGNS.Contains(ch))
                            {
                                done = true;
                                if (stringb.Length > 0)
                                {
                                    Token tok = TokenFactory.Build(stringb.ToString());
                                    tokens.Add(tok);
                                    previousToken = tok.GetTokenType();
                                    stringb.Clear();
                                }

                                Token tok2 = TokenFactory.Build(ch);
                                TokenType currentToken = tok2.GetTokenType();
                                tokens.Add(tok2);

                                if (previousToken.Equals(TokenType.Divide))
                                {
                                    if (currentToken.Equals(TokenType.Divide))
                                    {
                                        commentSingleOn = true;

                                        if (tokens.Count >= 2)
                                            tokens.RemoveRange(tokens.Count - 2, 2);
                                        else
                                            tokens.Clear();
                                    }
                                    if (currentToken.Equals(TokenType.Multiply))
                                    {
                                        commentMultilineOn = true;

                                        if (tokens.Count >= 2)
                                            tokens.RemoveRange(tokens.Count - 2, 2);
                                        else
                                            tokens.Clear();
                                    }
                                }
                                previousToken = tok2.GetTokenType();
                            }

                            if (!done)
                            {
                                stringb.Append(ch);
                            }
                        }
                    }
                }

                previousChar = ch;
            }

            if (stringb.Length > 0)
            {
                tokens.Add(TokenFactory.Build(stringb.ToString()));
            }

            /*
            code = Comments.Remove(code);
            code = EmptySpaces.Compress(code);

            List<Token> tokens = new List <Token>();
            StringBuilder stringb = new StringBuilder();
            bool quotationOn = false;

            for (int i = 0; i < code.Length; i++)
            {
                if (keySigns.Contains(code[i]))
                {
                    if (code[i].Equals('"'))
                    {
                        quotationOn = !quotationOn;
                        stringb.Append('"');
                        if (!quotationOn)
                        {
                            tokens.Add(TokenFactory.BuildQuotationToken(stringb.ToString()));
                            stringb.Clear();
                        }
                    }
                    else
                    {
                        if (!quotationOn)
                        {
                            if (stringb.ToString().Trim().Length > 0)
                                tokens.Add(TokenFactory.Build(stringb.ToString()));
                            stringb.Clear();

                            if (!code[i].Equals(' '))
                                tokens.Add(TokenFactory.Build(code[i]));
                        }
                        else
                            stringb.Append(code[i]);
                    }
                }
                else
                    stringb.Append(code[i]);
            }
            if (stringb.ToString().Trim().Length > 0)
                tokens.Add(TokenFactory.Build(stringb.ToString()));

            if (tokens.Count() == 0)
                throw new SyntaxErrorException("ERROR! Code is empty.");

            Brackets.InformAboutCorrectness(tokens);

            tokens = TokenModifier.VariablesToNumeric(tokens);
            tokens = TokenModifier.MergeTokens(tokens);

            return tokens;
            */

            Brackets.InformAboutCorrectness(tokens);

            tokens = TokenModifier.VariablesToNumeric(tokens);
            tokens = TokenModifier.MergeTokens(tokens);

            return tokens;
        }
    }
}
