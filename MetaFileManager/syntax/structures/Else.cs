using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.structures.abstracts;

namespace Uroboros.syntax.structures
{
    class Else : Structure
    {
        public Else()
        {
        }

        public override bool HasNext()
        {
            return false;
        }
    }
}
