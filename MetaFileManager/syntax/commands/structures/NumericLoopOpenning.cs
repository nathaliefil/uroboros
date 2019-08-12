using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.structures
{
    class NumericLoopOpenning : BracketOn, INumerable
    {
        private INumerable repeats;

        public NumericLoopOpenning(INumerable repeats, int commandNumber)
        {
            this.repeats = repeats;
            this.commandNumber = commandNumber;
        }

        public decimal ToNumber()
        {
            return repeats.ToNumber();
        }
    }
}
