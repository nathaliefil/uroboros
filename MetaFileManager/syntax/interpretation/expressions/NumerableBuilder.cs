using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;
using DivineScript.syntax.reading;

namespace DivineScript.syntax.interpretation.expressions
{
    class NumerableBuilder
    {
        public static INumerable Build(List<Token> tokens)
        {
            IBoolable ibo = BoolableBuilder.Build(tokens);
            if (!(ibo is NullVariable))
            {
                return ibo;
            }
                
            //code

            return new NumericConstant(0);
        }
    }
}
