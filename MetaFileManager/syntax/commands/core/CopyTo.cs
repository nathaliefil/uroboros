using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using System.IO;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.core
{
    class CopyTo : CoreCommand
    {
        private IStringable destination;
        private bool forced;

        public CopyTo(IListable list, IStringable destination, bool forced)
        {
            this.list = list;
            this.destination = destination;
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


            string oldLocation = rawLocation + "//" + fileName;
            string newLocation = rawLocation + "//" + directoryName + "//" + fileName;


            if (!Directory.Exists(rawLocation + "//" + directoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName);


            try
            {
                if (forced && File.Exists(newLocation))
                    File.Delete(@newLocation);
                File.Copy(@oldLocation, @newLocation);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Copy " + fileName + " to " + directoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during copying " + fileName + " to " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during copying " + fileName + " to " + directoryName + ".");
            }
        }

        protected override void DirectoryAction(string movingDirectoryName, string rawLocation)
        {
            string directoryName = destination.ToString();
            if (directoryName.Equals(movingDirectoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! Directory " + directoryName + " cannot be copied to itself.");
            }


            if (!FileValidator.IsNameCorrect(directoryName))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + directoryName + " contains not allowed characters.");
            }


            string oldLocation = rawLocation + "//" + movingDirectoryName;
            string newLocation = rawLocation + "//" + directoryName + "//" + movingDirectoryName;


            if (!Directory.Exists(rawLocation + "//" + directoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName);

            if (!Directory.Exists(rawLocation + "//" + directoryName + "//" + movingDirectoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName + "//" + movingDirectoryName);


            try
            {
                if (forced && Directory.Exists(newLocation))
                    Directory.Delete(@newLocation, true);
                DirectoryCopy(@oldLocation, @newLocation);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Copy " + movingDirectoryName + " to " + directoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during coping " + movingDirectoryName + " to " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during coping " + movingDirectoryName + " to " + directoryName + ".");
            }
        }

        private void DirectoryCopy(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                DirectoryCopy(directory, Path.Combine(dest, dirName));
            }

            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            }
        }
    }
}
