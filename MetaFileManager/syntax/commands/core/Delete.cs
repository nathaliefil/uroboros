using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.expressions;
using Uroboros.syntax.runtime;
using System.IO;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.core
{
    class Delete : CoreCommand
    {
        public Delete(IListable list)
        {
            this.list = list;
        }

        /// todo
    }
}
