using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;
using System.IO;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.commands.core
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
