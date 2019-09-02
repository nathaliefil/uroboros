using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.core
{
    class Unhide : CoreCommand
    {
        public Unhide(IListable list)
        {
            this.list = list;
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            string location = rawLocation + "\\" + directoryName;
            try
            {
                DirectoryInfo di = new DirectoryInfo(location);
                di.Attributes &= ~FileAttributes.Hidden;

                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Unhide " + directoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during unhiding " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during unhiding " + directoryName + ".");
            }
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;
            try
            {
                var attributes = File.GetAttributes(location);
                if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    attributes &= ~FileAttributes.Hidden;
                    File.SetAttributes(location, attributes);
                }

                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Unhide " + fileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during unhiding " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during unhiding " + fileName + ".");
            }
        }
    }
}
