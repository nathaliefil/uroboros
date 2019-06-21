using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;

namespace Uroboros.syntax.interpretation.functions
{
    public struct Argument
    {
        public List<Token> tokens;

        public Argument(List<Token> toks)
        {
            tokens = toks;
        }
    }
}
