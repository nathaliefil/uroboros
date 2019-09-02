using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.log;

namespace Uroboros.syntax.commands.core
{
    class Drop : CoreCommand
    {
        public Drop(IListable list)
        {
            this.list = list;
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            string location = rawLocation + "\\" + directoryName;
            try
            {
                Directory.Delete(@location, true);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Drop " + directoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during dropping " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during dropping " + directoryName + ".");
            }
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;
            try
            {
                File.Delete(@location);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Drop " + fileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during dropping " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during dropping " + fileName + ".");
            }
        }
    }
}
