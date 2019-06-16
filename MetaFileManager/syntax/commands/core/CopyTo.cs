using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.commands.core
{
    class CopyTo : CoreCommand
    {
        IStringable destination;
        bool forced;

        public CopyTo(IListable list, IStringable destination, bool forced)
        {
            this.list = list;
            this.destination = destination;
            this.forced = forced;
        }

        /*protected override void PerformAction(string element)
        {
            //code copying one file to another destination
        }*/

    }
}
