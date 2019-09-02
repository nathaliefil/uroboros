using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using System.IO;
using System.Collections.Specialized;
using System.Windows.Forms;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.log;

namespace Uroboros.syntax.commands.core
{
    class Copy : CoreCommand
    {
        public Copy(IListable list)
        {
            this.list = list;
        }

        protected override void DirectoryAction(string directoryName, string rawLocation)
        {
            FileAction(directoryName, rawLocation);
        }

        protected override void FileAction(string fileName, string rawLocation)
        {
            string location = rawLocation + "\\" + fileName;
            StringCollection paths = Clipboard.GetFileDropList();

            if (paths.Contains(location))
            {
                string s = FileValidator.IsDirectory(fileName) ? "Directory " : "File ";
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + s + fileName + " is already copied.");
            }

            try
            {
                paths.Add(location);
                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Copy " + fileName);
            }
            catch (Exception)
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! Something went wrong during copying " + fileName + ".");
            }

            Clipboard.SetFileDropList(paths);
        }
    }
}
