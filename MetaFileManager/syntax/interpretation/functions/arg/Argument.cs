using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;

namespace Uroboros.syntax.interpretation.functions.arg
{
    public struct Argument
    {
        public List<Token> tokens;

        public Argument(List<Token> tokens)
        {
            this.tokens = tokens;
        }
    }
}
