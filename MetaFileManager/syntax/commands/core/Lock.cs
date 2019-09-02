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
    class Lock : CoreCommand
    {
        public Lock(IListable list)
        {
            this.list = list;
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            string location = rawLocation + "\\" + directoryName;
            try
            {
                DirectoryInfo di = new DirectoryInfo(location);
                di.Attributes |= FileAttributes.ReadOnly;
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Lock " + directoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during locking " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during locking " + directoryName + ".");
            }
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;
            try
            {
                File.SetAttributes(location, File.GetAttributes(location) | FileAttributes.ReadOnly);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Lock " + fileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during locking " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during locking " + fileName + ".");
            }
        }
    }
}
