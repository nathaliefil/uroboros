using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.commands.core
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
