using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    class Reader
    {
        private static char[] keySigns = new char[] 
                {'!', '=', '(', ')', '{', '}', ';', 
                 '-', '+', '*', '\"', '%', '/', '&', '|', '^', ' '};


        public static List<Token> CreateTokenlist(string code)
        {
            code = Comments.Remove(code);

            List<Token> tokens = new List <Token>();
            StringBuilder stringb = new StringBuilder();
            bool quotationOn = false;

            for (int i = 0; i < code.Length; i++)
            {
                if (keySigns.Contains(code[i]))
                {
                    if (code[i].Equals('\"'))
                    {
                        quotationOn = !quotationOn;
                        stringb.Append('\"');
                        if (!quotationOn)
                        {
                            tokens.Add(TokenFactory.BuildQuotationToken(stringb.ToString()));
                            stringb.Clear();
                        }
                    }
                    else
                    {
                        if (stringb.Length > 0)
                        {
                            tokens.Add(TokenFactory.Build(stringb.ToString()));
                            stringb.Clear();
                        }
                        tokens.Add(TokenFactory.Build(code[i]));
                    }
                }
                else
                {
                    stringb.Append(code[i]);
                }
            }
            if (stringb.Length > 0)
            {
                tokens.Add(TokenFactory.Build(stringb.ToString()));
                stringb.Clear();
            }
            return tokens;
        }

    }
}
