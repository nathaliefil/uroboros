using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class CreateDirectory : CoreCommandCreate
    {
        public CreateDirectory(IListable list, bool forced)
        {
            this.list = list;
            this.forced = forced;
        }

        protected override void DirectoryAction(string directoryName, string location)
        {
            try
            {
                Directory.CreateDirectory(@location);
                Logger.GetInstance().LogCommand("Create directory " + directoryName);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during creating directory " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during creating directory " + directoryName + ".");
            }
        }

        protected override void FileAction(string fileName, string location)
        {
            throw new CommandException("Action ignored! Name for directory " + fileName + " is not suitable.");
        }
    }
}
