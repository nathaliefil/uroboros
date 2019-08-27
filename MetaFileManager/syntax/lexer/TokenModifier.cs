using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.lexer
{
    class TokenModifier
    {
        private static TokenType[][] TOKENS_TO_MERGE;
        // for declaration -> go down

        
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
            List<Token> newTokens = new List<Token>() ;
            bool tokensMerged = false;
            int i = 0;

            if (tokens.Count > 1)
            {
                for (i = 0; i < tokens.Count - 1; i++)
                {
                    tokensMerged = false;
                    int count = TOKENS_TO_MERGE.Count();

                    for (int j = 0; j < count; j++)
                    {
                        if (tokens[i].GetTokenType() == TOKENS_TO_MERGE[j][0] && tokens[i+1].GetTokenType() == TOKENS_TO_MERGE[j][1])
                        {
                            newTokens.Add(new Token(TOKENS_TO_MERGE[j][2]));
                            tokensMerged = true;
                            break;
                        }
                    }

                    if (tokensMerged)
                        i++;
                    else
                        newTokens.Add(tokens[i]);
                }
            }
            if (i < tokens.Count)
                newTokens.Add(tokens[tokens.Count-1]);

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


        static TokenModifier()
        {
            // first and second token is merged to third token
            TOKENS_TO_MERGE = new TokenType[25][]; //   TOKEN     +     TOKEN    =>      MERGED TWO
            TOKENS_TO_MERGE[0] = new TokenType[] { TokenType.Plus, TokenType.Plus, TokenType.PlusPlus };
            TOKENS_TO_MERGE[1] = new TokenType[] { TokenType.Minus, TokenType.Minus, TokenType.MinusMinus };
            TOKENS_TO_MERGE[2] = new TokenType[] { TokenType.Equals, TokenType.Equals, TokenType.Equals };
            TOKENS_TO_MERGE[3] = new TokenType[] { TokenType.And, TokenType.And, TokenType.And };
            TOKENS_TO_MERGE[4] = new TokenType[] { TokenType.Or, TokenType.Or, TokenType.Or };
            TOKENS_TO_MERGE[5] = new TokenType[] { TokenType.Bigger, TokenType.Equals, TokenType.BiggerOrEquals };
            TOKENS_TO_MERGE[6] = new TokenType[] { TokenType.Bigger, TokenType.Equals, TokenType.BiggerOrEquals };
            TOKENS_TO_MERGE[7] = new TokenType[] { TokenType.Smaller, TokenType.Equals, TokenType.SmallerOrEquals };
            TOKENS_TO_MERGE[8] = new TokenType[] { TokenType.Equals, TokenType.Bigger, TokenType.BigArrow };
            TOKENS_TO_MERGE[9] = new TokenType[] { TokenType.Minus, TokenType.Bigger, TokenType.SmallArrow };
            TOKENS_TO_MERGE[10] = new TokenType[] { TokenType.Exclamation, TokenType.Equals, TokenType.NotEquals };
            TOKENS_TO_MERGE[11] = new TokenType[] { TokenType.Is, TokenType.Not, TokenType.NotEquals };
            TOKENS_TO_MERGE[12] = new TokenType[] { TokenType.Force, TokenType.To, TokenType.ForceTo };
            TOKENS_TO_MERGE[13] = new TokenType[] { TokenType.Create, TokenType.File, TokenType.CreateFile };
            TOKENS_TO_MERGE[14] = new TokenType[] { TokenType.Create, TokenType.Directory, TokenType.CreateDirectory };
            TOKENS_TO_MERGE[15] = new TokenType[] { TokenType.Order, TokenType.By, TokenType.OrderBy };
            TOKENS_TO_MERGE[16] = new TokenType[] { TokenType.Sleep, TokenType.For, TokenType.Sleep };
            TOKENS_TO_MERGE[17] = new TokenType[] { TokenType.Plus, TokenType.Equals, TokenType.PlusEquals };
            TOKENS_TO_MERGE[18] = new TokenType[] { TokenType.Minus, TokenType.Equals, TokenType.MinusEquals };
            TOKENS_TO_MERGE[19] = new TokenType[] { TokenType.Multiply, TokenType.Equals, TokenType.MultiplyEquals };
            TOKENS_TO_MERGE[20] = new TokenType[] { TokenType.Divide, TokenType.Equals, TokenType.DivideEquals };
            TOKENS_TO_MERGE[21] = new TokenType[] { TokenType.Percent, TokenType.Equals, TokenType.PercentEquals };
            TOKENS_TO_MERGE[22] = new TokenType[] { TokenType.Is, TokenType.Before, TokenType.IsBefore };
            TOKENS_TO_MERGE[23] = new TokenType[] { TokenType.Is, TokenType.After, TokenType.IsAfter };
            TOKENS_TO_MERGE[24] = new TokenType[] { TokenType.Is, TokenType.Between, TokenType.Between };
        }
    }
}
