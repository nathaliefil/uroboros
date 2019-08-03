using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class CreateFileFrom : CoreCommandCreate
    {
        private IStringable from;

        public CreateFileFrom(IStringable from, IListable list, bool forced)
        {
            this.from = from;
            this.list = list;
            this.forced = forced;
        }

        protected override void DirectoryAction(string directoryName, string newLocation)
        {
            throw new CommandException("Action ignored! Name for file " + directoryName + " is not suitable.");
        }

        protected override void FileAction(string fileName, string newLocation)
        {
            string source = from.ToString();
            string sourceLocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + source;

            if (!FileValidator.IsNameCorrect(source))
                throw new CommandException("Action ignored! Source file name in creation of file " + fileName + " contains not allowed characters.");

            if (!File.Exists(sourceLocation))
                throw new CommandException("Action ignored! Source file " + source + " do not exist.");


            try
            {
                File.Copy(@sourceLocation, @newLocation);
                Logger.GetInstance().LogCommand("Create file " + fileName);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during creating file " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during creating file " + fileName + ".");
            }
        }
    }
}
