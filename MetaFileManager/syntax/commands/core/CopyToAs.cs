using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using System.IO;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.from_file;
using Uroboros.syntax.log;

namespace Uroboros.syntax.commands.core
{
    class CopyToAs : CoreCommand
    {
        private IStringable destination;
        private IStringable newName;
        private bool forced;

        public CopyToAs(IListable list, IStringable destination, IStringable newName, bool forced)
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
                string extension = FileInnerVariable.GetExtension(fileName);
                newFileName += "." + extension;
            }


            string oldLocation = rawLocation + "//" + fileName;
            string newLocation = rawLocation + "//" + directoryName + "//" + newFileName;


            if (!Directory.Exists(rawLocation + "//" + directoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName);


            try
            {
                if (forced && File.Exists(newLocation))
                    File.Delete(@newLocation);
                File.Copy(@oldLocation, @newLocation);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Copy " + fileName + " to " + directoryName + " as " + newFileName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during copying " + fileName + " to " + directoryName + " as " + newFileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during copying " + fileName + " to " + directoryName + " as " + newFileName + ".");
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

            if (!Directory.Exists(rawLocation + "//" + directoryName + "//" + newMovingDirectoryName))
                Directory.CreateDirectory(rawLocation + "//" + directoryName + "//" + newMovingDirectoryName);


            try
            {
                if (forced && Directory.Exists(newLocation))
                    Directory.Delete(@newLocation, true);
                DirectoryCopy(@oldLocation, @newLocation);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Copy " + movingDirectoryName + " to " + directoryName + " as " + newMovingDirectoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during coping " + movingDirectoryName + " to " + directoryName + " as " + newMovingDirectoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during coping " + movingDirectoryName + " to " + directoryName + " as " + newMovingDirectoryName + ".");
            }
        }

        private void DirectoryCopy(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = System.IO.Path.GetFileName(directory);
                if (!Directory.Exists(System.IO.Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(dest, dirName));
                }
                DirectoryCopy(directory, System.IO.Path.Combine(dest, dirName));
            }

            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, System.IO.Path.Combine(dest, System.IO.Path.GetFileName(file)));
            }
        }
    }
}
