using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.core
{
    class Select: CoreCommand
    {
        public Select(IListable list)
        {
            this.list = list;
        }

        public override void Run ()
        {


            //code selecting files
        }

    }
}
