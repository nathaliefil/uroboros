using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax
{
    partial class Syntax
    {
        public static bool CorrectBrackets(string code)
        {
            int number = 0;

            for (int i = 0; i < code.Length; i++)
            {
                if (code[i].Equals('('))
                {
                    number++;
                }
                if (code[i].Equals(')'))
                {
                    number--;
                    if (number < 0)
                    {
                        return false;
                    }
                }
            }
            if (number == 0)
                return true;
            else
                return false;
        }

        public static bool CorrectQuotations(string code)
        {
            int number = 0;

            for (int i = 0; i < code.Length; i++)
            {
                if (code[i].Equals('"'))
                {
                    number++;
                }
            }
            if (number % 2 == 0)
                return true;
            else
                return false;
        }
    }
}
