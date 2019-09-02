using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;
using Uroboros.syntax.variables.from_file;
using Uroboros.syntax.log;

namespace Uroboros.syntax.commands.core
{
    class RemodifyTo : CoreCommand
    {
        private ITimeable newTime;

        public RemodifyTo(IListable list, ITimeable newTime)
        {
            this.list = list;
            this.newTime = newTime;
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;

            try
            {
                File.SetLastWriteTime(@location, newTime.ToTime());
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Modification of " + fileName + " is now " + newTime.ToString());
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during changing modification time of " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during changing modification time of " + fileName + ".");
            }
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            string location = rawLocation + "\\" + directoryName;

            try
            {
                Directory.SetLastWriteTime(@location, newTime.ToTime());
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Modification of " + directoryName + " is now " + newTime.ToString());
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during changing modification time of " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during changing modification time of " + directoryName + ".");
            }
        }
    }
}
