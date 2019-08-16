using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.reading
{
    class Reader
    {
        private static char[] KEY_SIGNS = new char[] 
                {',', '!', '=', '(', ')', '{', '}', '[', ']',';', ':',
                 '-', '+', '*', '%', '/', '&', '|', '^', '<', '>', '?'};


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
                        tokens.Add(new Token(TokenType.StringConstant, stringb.ToString()));
                        previousToken = TokenType.StringConstant;
                        stringb.Clear();
                        quotationOn = false;
                    }
                    else
                        stringb.Append(ch);
                }
                else
                {
                    if (commentSingleOn)
                    {
                        if (ch == '\n' || ch=='\t' || ch=='\r' || ch.Equals("\r\n"))
                            commentSingleOn = false;
                    }
                    else
                    {
                        if (commentMultilineOn)
                        {
                            if (previousChar == '*' && ch == '/')
                                commentMultilineOn = false;
                        }
                        else
                        {
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
                                stringb.Append(ch);
                        }
                    }
                }

                previousChar = ch;
            }

            if (stringb.Length > 0)
            {
                tokens.Add(TokenFactory.Build(stringb.ToString()));
            }
             
            Brackets.InformAboutCorrectness(tokens);

            tokens = TokenModifier.VariablesToNumeric(tokens);
            tokens = TokenModifier.MergeTokens(tokens);

            return tokens;
        }
    }
}
