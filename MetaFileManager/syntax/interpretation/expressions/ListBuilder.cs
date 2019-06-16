using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;
using DivineScript.syntax.reading;

namespace DivineScript.syntax.interpretation.expressions
{
    class ListBuilder
    {
        public static IListable Build(List<Token> tokens)
        {
            //code

            return new ListConstant(new List<string>());
        }
    }
}
