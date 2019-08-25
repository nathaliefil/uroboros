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

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            FileAction(directoryName, rawLocation);
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;

            try
            {
                Process.Start(@location);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Open " + fileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during openning " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during openning " + fileName + ".");
            }
        }
    }
}
