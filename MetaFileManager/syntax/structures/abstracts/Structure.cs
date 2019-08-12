using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.structures.abstracts
{
    abstract class Structure
    {
        protected int commandNumber;

        abstract public bool HasNext();

        public int GetCommandNumber()
        {
            return commandNumber;
        }
    }
}
