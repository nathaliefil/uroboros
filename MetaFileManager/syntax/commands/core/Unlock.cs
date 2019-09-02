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
    class Unlock : CoreCommand
    {
        public Unlock(IListable list)
        {
            this.list = list;
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            string location = rawLocation + "\\" + directoryName;
            try
            {
                DirectoryInfo di = new DirectoryInfo(location);
                di.Attributes &= ~FileAttributes.ReadOnly;

                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Unlock " + directoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during unlocking " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during unlocking " + directoryName + ".");
            }
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;
            try
            {
                var attributes = File.GetAttributes(location);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes &= ~FileAttributes.ReadOnly;
                    File.SetAttributes(location, attributes);
                }

                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Unlock " + fileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during unlocking " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during unlocking " + fileName + ".");
            }
        }
    }
}
