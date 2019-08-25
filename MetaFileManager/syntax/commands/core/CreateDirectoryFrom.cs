using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class CreateDirectoryFrom : CoreCommandCreate
    {
        private IStringable from;

        public CreateDirectoryFrom(IStringable from, IListable list, bool forced)
        {
            this.from = from;
            this.list = list;
            this.forced = forced;
        }

        protected override void DirectoryAction(string directoryName, string newLocation)
        {
            string source = from.ToString();
            string sourceLocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + source;

            if (!FileValidator.IsNameCorrect(source))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! Source directory name in creation of directory " + directoryName + " contains not allowed characters.");
            }

            if (!Directory.Exists(sourceLocation))
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! Source directory " + source + " do not exist.");
            }


            try
            {
                DirectoryCopy(@sourceLocation, @newLocation);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Create directory " + directoryName);
            }
            catch (Exception ex)
            {
                RuntimeVariables.GetInstance().Failure();

                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during creating directory " + directoryName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during creating directory " + directoryName + ".");
            }
        }

        protected override void FileAction(string fileName, string newLocation)
        {
            RuntimeVariables.GetInstance().Failure();
            throw new CommandException("Action ignored! Name for directory " + fileName + " is not suitable.");
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
