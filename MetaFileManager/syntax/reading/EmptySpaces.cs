using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.reading
{
    class EmptySpaces
    {
        public static string Compress(string code)
        {
            // change all tabs and newlines into spaces
            // only if they are outside quotation marks

            StringBuilder stringb = new StringBuilder();
            bool quotationOn = false;

            for (int i = 0; i < code.Length; i++)
            {
                if (code[i].Equals('"'))
                {
                    quotationOn = !quotationOn;
                }
                if (!quotationOn && !code[i].Equals('\n') && !code[i].Equals('\t')
                    && !code[i].Equals('\r') && !code[i].Equals("\r\n")
                    )
                {
                    stringb.Append(code[i]);
                }
            }
            return stringb.ToString(); ;
        }
    }
}
