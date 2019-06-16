using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;
using DivineScript.syntax.reading;

namespace DivineScript.syntax.interpretation.expressions
{
    class StringableBuilder
    {
        public static IStringable Build(List<Token> tokens)
        {
            INumerable inu = NumerableBuilder.Build(tokens);
            if (!(inu is NullVariable))
            {
                return inu;
            }

            //code

            return new StringConstant("");
        }
    }
}
