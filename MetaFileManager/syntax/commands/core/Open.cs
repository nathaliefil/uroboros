using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;
using System.IO;
using System.Diagnostics;

namespace DivineScript.syntax.commands.core
{
    class Open : CoreCommand
    {
        public Open(ListExpression list)
        {
            this.list = list;
        }

        protected override void PerformDirectoryAction(string element, string location)
        {
            PerformFileAction(element, location);
        }

        protected override void PerformFileAction(string element, string location)
        {
            Process.Start(@location);
            Logger.GetInstance().Log("Open " + element);
        }
    }
}
