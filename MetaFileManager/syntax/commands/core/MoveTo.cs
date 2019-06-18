﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.core
{
    class MoveTo : CoreCommand
    {
        IStringable destination;
        bool forced;

        public MoveTo(IListable list, IStringable destination, bool forced)
        {
            this.list = list;
            this.destination = destination;
            this.forced = forced;
        }

        /*protected override void PerformAction(string element)
        {
            //code moving one file to another destination
        }*/

    }
}
