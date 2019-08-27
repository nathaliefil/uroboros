using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class MoveToAs : CoreCommand
    {
        private IStringable destination;
        private IStringable newName;
        private bool forced;

        public MoveToAs(IListable list, IStringable destination, IStringable newName, bool forced)
        {
            this.list = list;
            this.destination = destination;
            this.newName = newName;
            this.forced = forced;
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string directoryName = destination.ToString();
            if (!FileValidator.IsNameCorrect(directoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + directoryName + " contains not allowed characters.");
            }

            string newFileName = newName.ToString();
            if (!FileValidator.IsNameCorrect(newFileName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + newFileName + " contains not allowed characters.");
            }
            if (FileValidator.IsDirectory(newFileName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + newFileName + " is not allowed name for file.");
            }

            string oldLocation = rawLocation + "//" + fileName;
            string newLocation = rawLocation + "//" + directoryName + "//" + newFileName;


            if (!Directory.Exists(rawLocation + "//" + directoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName);


            try
            {
                if (forced && File.Exists(newLocation))
                    File.Delete(@newLocation);
                File.Move(@oldLocation, @newLocation);

                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Move " + fileName + " to " + directoryName + "as" + newFileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during moving " + fileName + " to " + directoryName + "as" + newFileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during moving " + fileName + " to " + directoryName + "as" + newFileName + ".");
            }
        }

        protected override void DirectoryAction(string movingDirectoryName, string rawLocation)
        {
            string directoryName = destination.ToString();
            if (directoryName.Equals(movingDirectoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! Directory " + directoryName + " cannot be moved to itself.");
            }

            if (!FileValidator.IsNameCorrect(directoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + directoryName + " contains not allowed characters.");
            }

            string newMovingDirectoryName = newName.ToString();
            if (!FileValidator.IsNameCorrect(newMovingDirectoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + newMovingDirectoryName + " contains not allowed characters.");
            }
            if (!FileValidator.IsDirectory(newMovingDirectoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + newMovingDirectoryName + " is not allowed name for directory.");
            }


            string oldLocation = rawLocation + "//" + movingDirectoryName;
            string newLocation = rawLocation + "//" + directoryName + "//" + newMovingDirectoryName;


            if (!Directory.Exists(rawLocation + "//" + directoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName);


            try
            {
                if (forced && Directory.Exists(newLocation))
                    Directory.Delete(@newLocation, true);
                Directory.Move(@oldLocation, @newLocation);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Move " + movingDirectoryName + " to " + directoryName + "as" + newMovingDirectoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during moving " + movingDirectoryName + " to " + directoryName + "as" + newMovingDirectoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during moving " + movingDirectoryName + " to " + directoryName + "as" + newMovingDirectoryName + ".");
            }
        }
    }
}
