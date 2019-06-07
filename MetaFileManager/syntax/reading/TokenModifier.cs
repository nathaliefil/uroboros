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
                    t.PointToComma();
                    decimal value;
                    if (Decimal.TryParse(t.GetContent(), out value))
                        t.SetToNumericConstant();
                }
            }
            return tokens;
        }

        public static List<Token> MergeTokens (List<Token> tokens)
        {




            return tokens;
        }

    }
}
