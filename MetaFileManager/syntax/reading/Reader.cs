using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax;

namespace Uroboros.syntax.reading
{
    class Reader
    {
        private static char[] keySigns = new char[] 
                {',', '!', '=', '(', ')', '{', '}', '[', ']',';', ':',
                 '-', '+', '*', '"', '%', '/', '&', '|', '^', ' ', '<', '>'};


        public static List<Token> CreateTokenlist(string code)
        {
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

            Brackets.CheckCorrectness(tokens, true);

            tokens = TokenModifier.VariablesToNumeric(tokens);
            tokens = TokenModifier.MergeTokens(tokens);

            return tokens;
        }
    }
}
