using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.lexer
{
    class TokenModifier
    {
        public static List<Token> VariablesToNumeric (List<Token> tokens)
        {
            foreach (Token t in tokens)
            {
                if (t.GetTokenType() == TokenType.Variable)
                {
                    if (ParsableToNumber(t.GetContent()))
                        t.SetToNumericConstant();
                    else
                    {
                        string content = t.GetContent();
                        if (content.Length > 2)
                        {
                            SizeSufix ssfx = GetSizeSufix(content.Substring(content.Length - 2));

                            if (ssfx != SizeSufix.None)
                            {
                                string mainPart = content.Substring(0, content.Length - 2);
                                if (ParsableToNumber(mainPart))
                                    t.SetToNumericConstant(ssfx);
                            }
                        }
                        if (content.Length > 1)
                        {
                            char s = content[content.Length-1];

                            if (s.Equals('k') || s.Equals('K'))
                            {
                                string mainPart = content.Substring(0, content.Length - 1);
                                if (ParsableToNumber(mainPart))
                                    t.SetToNumericConstant(SizeSufix.K);
                            }
                        }

                    }
                }
            }
            return tokens;
        }

        private static bool ParsableToNumber (string s)
        {
            string ss = s.Replace('.', ',');
            decimal value;
            if (Decimal.TryParse(ss, out value))
                return true;
            else
                return false;
        }

        public static List<Token> MergeTokens (List<Token> tokens)
        {
            // this method needs serious refactoring
            /// todo

            List<Token> newTokens = new List<Token>() ;
            bool tokensMerged = false;
            int i = 0;

            if (tokens.Count > 1)
            {
                for (i = 0; i < tokens.Count - 1; i++)
                {
                    tokensMerged = false;
                    if (tokens[i].GetTokenType() == tokens[i+1].GetTokenType())
                    {
                        if (tokens[i].GetTokenType() == TokenType.Plus)
                        {
                            newTokens.Add(new Token(TokenType.PlusPlus));
                            tokensMerged = true;
                        }
                        if (tokens[i].GetTokenType() == TokenType.Minus)
                        {
                            newTokens.Add(new Token(TokenType.MinusMinus));
                            tokensMerged = true;
                        }
                        if (tokens[i].GetTokenType() == TokenType.Equals)
                        {
                            newTokens.Add(new Token(TokenType.Equals));
                            tokensMerged = true;
                        }
                        if (tokens[i].GetTokenType() == TokenType.And)
                        {
                            newTokens.Add(new Token(TokenType.And));
                            tokensMerged = true;
                        }
                        if (tokens[i].GetTokenType() == TokenType.Or)
                        {
                            newTokens.Add(new Token(TokenType.Or));
                            tokensMerged = true;
                        }
                    }
                    if (tokens[i].GetTokenType() == TokenType.Bigger && tokens[i+1].GetTokenType() == TokenType.Equals)
                    {
                        newTokens.Add(new Token(TokenType.BiggerOrEquals));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Smaller && tokens[i+1].GetTokenType() == TokenType.Equals)
                    {
                        newTokens.Add(new Token(TokenType.SmallerOrEquals));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Equals && tokens[i+1].GetTokenType() == TokenType.Bigger)
                    {
                        newTokens.Add(new Token(TokenType.BigArrow));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Minus && tokens[i + 1].GetTokenType() == TokenType.Bigger)
                    {
                        newTokens.Add(new Token(TokenType.SmallArrow));
                        tokensMerged = true;
                    }
                    if ((tokens[i].GetTokenType() == TokenType.Exclamation && tokens[i + 1].GetTokenType() == TokenType.Equals)||
                        (tokens[i].GetTokenType() == TokenType.Is && tokens[i + 1].GetTokenType() == TokenType.Exclamation))
                    {
                        newTokens.Add(new Token(TokenType.NotEquals));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Force && tokens[i + 1].GetTokenType() == TokenType.To)
                    {
                        newTokens.Add(new Token(TokenType.ForceTo));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Create && tokens[i + 1].GetTokenType() == TokenType.File)
                    {
                        newTokens.Add(new Token(TokenType.CreateFile));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Create && tokens[i + 1].GetTokenType() == TokenType.Directory)
                    {
                        newTokens.Add(new Token(TokenType.CreateDirectory));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Order && tokens[i + 1].GetTokenType() == TokenType.By)
                    {
                        newTokens.Add(new Token(TokenType.OrderBy));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Sleep && tokens[i + 1].GetTokenType() == TokenType.For)
                    {
                        newTokens.Add(new Token(TokenType.Sleep));
                        tokensMerged = true;
                    }


                    if (tokens[i].GetTokenType() == TokenType.Plus && tokens[i + 1].GetTokenType() == TokenType.Equals)
                    {
                        newTokens.Add(new Token(TokenType.PlusEquals));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Minus && tokens[i + 1].GetTokenType() == TokenType.Equals)
                    {
                        newTokens.Add(new Token(TokenType.MinusEquals));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Multiply && tokens[i + 1].GetTokenType() == TokenType.Equals)
                    {
                        newTokens.Add(new Token(TokenType.MultiplyEquals));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Divide && tokens[i + 1].GetTokenType() == TokenType.Equals)
                    {
                        newTokens.Add(new Token(TokenType.DivideEquals));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Percent && tokens[i + 1].GetTokenType() == TokenType.Equals)
                    {
                        newTokens.Add(new Token(TokenType.PercentEquals));
                        tokensMerged = true;
                    }



                    if (tokens[i].GetTokenType() == TokenType.Is && tokens[i + 1].GetTokenType() == TokenType.Before)
                    {
                        newTokens.Add(new Token(TokenType.IsBefore));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Is && tokens[i + 1].GetTokenType() == TokenType.After)
                    {
                        newTokens.Add(new Token(TokenType.IsAfter));
                        tokensMerged = true;
                    }



                    if (tokensMerged)
                        i++;
                    else
                        newTokens.Add(tokens[i]);
                }
            }
            if (i < tokens.Count)
            {
                newTokens.Add(tokens[tokens.Count-1]);
            }

            foreach (Token tok in newTokens)
            {
                tok.ChangeTokenType();
            }


            return newTokens;
        }

        private static SizeSufix GetSizeSufix(string str)
        {
            switch (str)
            {
                case "kB":
                    return SizeSufix.KB;
                case "kb":
                    return SizeSufix.KB;
                case "mb":
                    return SizeSufix.MB;
                case "gb":
                    return SizeSufix.GB;
                case "tb":
                    return SizeSufix.TB;
                case "pb":
                    return SizeSufix.PB;
                case "KB":
                    return SizeSufix.KB;
                case "MB":
                    return SizeSufix.MB;
                case "GB":
                    return SizeSufix.GB;
                case "TB":
                    return SizeSufix.TB;
                case "PB":
                    return SizeSufix.PB;
                case "kk":
                    return SizeSufix.KK;
                case "KK":
                    return SizeSufix.KK;
            }
            return SizeSufix.None;
        }
    }
}
