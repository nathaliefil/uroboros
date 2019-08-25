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
    class RenameTo : CoreCommand
    {
        private IStringable destination;
        private bool forced;

        public RenameTo(IListable list, IStringable destination, bool forced)
        {
            this.list = list;
            this.destination = destination;
            this.forced = forced;
        }

        protected override void FileAction(string oldFileName, string rawLocation)
        {
            string newFileName = destination.ToString();
            if (!FileValidator.IsNameCorrect(newFileName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + newFileName + " contains not allowed characters.");
            }

            if (FileValidator.IsDirectory(newFileName))
            {
                string extension = FileInnerVariable.GetExtension(oldFileName);
                newFileName += "." + extension;
            }

            if (oldFileName.Equals(newFileName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! Old name and new name for " + newFileName + " are the same and renaming is unnecessary.");
            }


            string slocation = rawLocation + "//" + oldFileName;
            string nlocation = rawLocation + "//" + newFileName;

            try
            {
                if (forced && File.Exists(nlocation))
                    File.Delete(@nlocation);
                File.Move(@slocation, @nlocation);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Rename " + oldFileName + " to " + newFileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during renaming " + oldFileName + " to " + newFileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during renaming " + oldFileName + " to " + newFileName + ".");
            }
        }

        protected override void DirectoryAction(string oldDirectoryName, string rawLocation)
        {
            string newDirectoryName = destination.ToString();
            if (!FileValidator.IsNameCorrect(newDirectoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + newDirectoryName + " contains not allowed characters.");
            }

            if (!FileValidator.IsDirectory(newDirectoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + newDirectoryName + ", the new name for directory " + oldDirectoryName + " is unsuitable. ");
            }

            if (oldDirectoryName.Equals(newDirectoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! Old name and new name for " + newDirectoryName + " are the same and renaming is unnecessary.");
            }

            string slocation = rawLocation + "//" + oldDirectoryName;
            string nlocation = rawLocation + "//" + newDirectoryName;

            try
            {
                if (forced && Directory.Exists(nlocation))
                    Directory.Delete(@nlocation, true);
                Directory.Move(@slocation, @nlocation);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Rename " + oldDirectoryName + " to " + newDirectoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during renaming " + oldDirectoryName + " to " + newDirectoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during renaming " + oldDirectoryName + " to " + newDirectoryName + ".");
            }
        }
    }
}
