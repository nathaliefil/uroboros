using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    class TokenModifier
    {
        public static List<Token> VariablesToNumeric (List<Token> tokens)
        {
            foreach (Token t in tokens)
            {
                if (t.GetTokenType() == TokenType.Variable)
                {
                    string ss = (t.GetContent()).Replace('.', ',');
                    decimal value;
                    if (Decimal.TryParse(ss, out value))
                    {
                        t.PointToComma();
                        t.SetToNumericConstant();
                    }
                }
            }
            return tokens;
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
                            newTokens.Add(new Token(TokenType.EqualsEquals));
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
                        newTokens.Add(new Token(TokenType.Arrow));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Else && tokens[i + 1].GetTokenType() == TokenType.If)
                    {
                        newTokens.Add(new Token(TokenType.Elif));
                        tokensMerged = true;
                    }
                    if (tokens[i].GetTokenType() == TokenType.Exclamation && tokens[i + 1].GetTokenType() == TokenType.Equals)
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

            return newTokens;
        }

    }
}
