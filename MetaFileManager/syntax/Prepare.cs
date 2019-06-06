using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DivineScript.syntax
{
    partial class Syntax
    {
        // here code without comments is modified to make it easier for computer to interpret
        // all whitespace characters are changed to spaces
        // before openning brackets and after ending brackets appears additional space
        // the same happens for quotation marks
        // multiple spaces are cut to one space
        public static string PrepareCode(string code)
        {
            code = EntersTabsToSpaces(code);
            String newcode = code;
            int moved = 0;
            int quotation = 0;
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == '(')
                {
                    newcode=newcode.Insert(i + moved, " ");
                    moved++;
                }
                if (code[i] == ')')
                {
                    newcode=newcode.Insert(i + 1 + moved, " ");
                    moved++;
                }
                if (code[i] == '"')
                {
                    if (quotation % 2 == 0)
                    {
                        newcode = newcode.Insert(i + moved, " ");
                    }
                    else
                    {
                        newcode = newcode.Insert(i + 1 + moved, " ");
                    }
                    quotation++;
                    moved++;
                }
            }
            return Regex.Replace(newcode, @"\s+", " ");
        }

        private static string EntersTabsToSpaces(string code)
        {
            char tab = '\u0009';
            code = code.Replace(tab.ToString(), " ");
            return code.Replace(System.Environment.NewLine, " ");
        }
    }
}
