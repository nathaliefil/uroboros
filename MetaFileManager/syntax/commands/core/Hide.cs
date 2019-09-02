using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.core
{
    class Hide : CoreCommand
    {
        public Hide(IListable list)
        {
            this.list = list;
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            string location = rawLocation + "\\" + directoryName;
            try
            {
                DirectoryInfo di = new DirectoryInfo(location);
                di.Attributes |= FileAttributes.Hidden;
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Hide " + directoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during hiding " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during hiding " + directoryName + ".");
            }
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;
            try
            {
                File.SetAttributes(location, File.GetAttributes(location) | FileAttributes.Hidden);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Hide " + fileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during hiding " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during hiding " + fileName + ".");
            }
        }
    }
}
