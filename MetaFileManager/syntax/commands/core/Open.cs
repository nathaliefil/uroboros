using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;
using System.Diagnostics;

namespace Uroboros.syntax.commands.core
{
    class Open : CoreCommand
    {
        public Open(IListable list)
        {
            this.list = list;
        }

        protected override void DirectoryAction(string directoryName, string location)
        {
            FileAction(directoryName, location);
        }

        protected override void FileAction(string fileName, string location)
        {
            try
            {
                Process.Start(@location);
                Logger.GetInstance().LogCommand("Open " + fileName);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during openning " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during openning " + fileName + ".");
            }
            
        }
    }
}
