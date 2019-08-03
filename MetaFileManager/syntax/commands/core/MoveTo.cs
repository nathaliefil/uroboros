using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class MoveTo : CoreCommand
    {
        private IStringable destination;
        private bool forced;

        public MoveTo(IListable list, IStringable destination, bool forced)
        {
            this.list = list;
            this.destination = destination;
            this.forced = forced;
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string directoryName = destination.ToString();
            if (!FileValidator.IsNameCorrect(directoryName))
                throw new CommandException("Action ignored! " + directoryName + " contains not allowed characters.");


            string oldLocation = rawLocation + "//" + fileName;
            string newLocation = rawLocation + "//" + directoryName + "//" + fileName;


            if (!Directory.Exists(rawLocation + "//" + directoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName);


            try
            {
                if (forced && File.Exists(newLocation))
                    File.Delete(@newLocation);
                File.Move(@oldLocation, @newLocation);
                Logger.GetInstance().LogCommand("Move " + fileName + " to " + directoryName);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during moving " + fileName + " to " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during moving " + fileName + " to " + directoryName + ".");
            }
        }

        protected override void DirectoryAction(string movingDirectoryName, string rawLocation)
        {
            string directoryName = destination.ToString();
            if (directoryName.Equals(movingDirectoryName))
                throw new CommandException("Action ignored! Directory " + directoryName + " cannot be moved to itself.");


            if (!FileValidator.IsNameCorrect(directoryName))
                throw new CommandException("Action ignored! " + directoryName + " contains not allowed characters.");


            string oldLocation = rawLocation + "//" + movingDirectoryName;
            string newLocation = rawLocation + "//" + directoryName + "//" + movingDirectoryName;


            if (!Directory.Exists(rawLocation + "//" + directoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName);


            try
            {
                if (forced && Directory.Exists(newLocation))
                    Directory.Delete(@newLocation, true);
                Directory.Move(@oldLocation, @newLocation);
                Logger.GetInstance().LogCommand("Move " + movingDirectoryName + " to " + directoryName);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during moving " + movingDirectoryName + " to " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during moving " + movingDirectoryName + " to " + directoryName + ".");
            }
        }
    }
}
