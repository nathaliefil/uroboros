using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class CreateFile : CoreCommandCreate
    {
        public CreateFile(IListable list, bool forced)
        {
            this.list = list;
            this.forced = forced;
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            throw new CommandException("Action ignored! Name for file " + directoryName + " is not suitable.");
        }

        protected override void FileAction(string fileName, string location)
        {
            try
            {
                File.Create(@location);
                Logger.GetInstance().LogCommand("Create file " + fileName);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during creating file " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during creating file " + fileName + ".");
            }
        }
    }
}
