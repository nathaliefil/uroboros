using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.numeric
{
    class Count : DefaultNumerable
    {
        private IListable list;

        public Count(IListable list)
        {
            this.list = list;
        }

        public override decimal ToNumber()
        {
            return list.ToList().Count;
        }
    }
}
