using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;
using Uroboros.syntax.variables.from_file;

namespace Uroboros.syntax.commands.core
{
    class ReaccessTo : CoreCommand
    {
        private ITimeable newTime;

        public ReaccessTo(IListable list, ITimeable newTime)
        {
            this.list = list;
            this.newTime = newTime;
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;

            try
            {
                File.SetLastAccessTime(@location, newTime.ToTime());
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Access of " + fileName + " is now " + newTime.ToString());
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during changing access time of " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during changing access time of " + fileName + ".");
            }
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            string location = rawLocation + "\\" + directoryName;

            try
            {
                Directory.SetLastAccessTime(@location, newTime.ToTime());
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Access of " + directoryName + " is now " + newTime.ToString());
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during changing access time of " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during changing access time of " + directoryName + ".");
            }
        }
    }
}
