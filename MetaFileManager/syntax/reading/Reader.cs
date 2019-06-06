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
                 '-', '+', '*', '\'', '%', '/', '&', '|', ' '};


        public static void CreateTokenlist(string code)
        {
            List<Token> tokens = new List <Token>();
            StringBuilder stringb = new StringBuilder();

            for (int i = 0; i < code.Length; i++)
            {
                if (keySigns.Contains(code[i]))
                {
                    if (stringb.Length > 0)
                    {
                        // create token from current cumulated string
                    }
                    // create token from code[i]
                }
                else
                {
                    stringb.Append(code[i]);
                }
            }
            if (stringb.Length > 0)
            {
                // create token from current cumulated string
            }
            //return list of tokens
        }

    }
}
