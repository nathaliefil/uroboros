using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.core
{
    class CopyTo : CoreCommand
    {
        private IStringable destination;
        private bool forced;

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
