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
    class RecreateTo : CoreCommand
    {
        private ITimeable newTime;

        public RecreateTo(IListable list, ITimeable newTime)
        {
            this.list = list;
            this.newTime = newTime;
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;

            try
            {
                File.SetCreationTime(@location, newTime.ToTime());
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Modification of " + fileName + " is now " + newTime.ToString());
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during changing creation time of " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during changing creation time of " + fileName + ".");
            }
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            string location = rawLocation + "\\" + directoryName;

            try
            {
                Directory.SetCreationTime(@location, newTime.ToTime());
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Creation of " + directoryName + " is now " + newTime.ToString());
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during changing creation time of " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during changing creation time of " + directoryName + ".");
            }
        }
    }
}
