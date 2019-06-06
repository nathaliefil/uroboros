using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax
{
    partial class Syntax
    {
        public static string RemoveComments(string code)
        {
            StringBuilder cleanCode = new StringBuilder();
            bool isComment = false;
            int countdownAfterEnd = 0;

            for (int i = 0; i < code.Length-1; i++)
            {
                if (code[i].Equals('/') && code[i+1].Equals('*'))
                {
                    isComment = true;
                }
                if (code[i].Equals('*') && code[i + 1].Equals('/'))
                {
                    isComment = false;
                    countdownAfterEnd = 2;
                }
                if (!isComment && countdownAfterEnd == 0)
                {
                    cleanCode.Append(code[i]);
                }
                if (countdownAfterEnd > 0)
                {
                    countdownAfterEnd--;
                }
            }
            if (!isComment && countdownAfterEnd == 0)
            {
                cleanCode.Append(code[code.Length - 1]);
            }

            string code2 = cleanCode.ToString();
            cleanCode.Clear();

            for (int i = 0; i < code2.Length - 1; i++)
            {
                if (code2[i].Equals('/') && code2[i + 1].Equals('/'))
                {
                    isComment = true;
                }

                if (!isComment)
                {
                    cleanCode.Append(code2[i]);
                }

                if (code2[i].Equals('\n') && isComment)
                {
                    
                    isComment = false;
                    cleanCode.Append('\n');
                }
            }
            if (!isComment)
            {
                cleanCode.Append(code2[code2.Length - 1]);
            }
            
            return cleanCode.ToString();
        }
    }
}
