using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using System.Collections.Specialized;
using Uroboros.syntax.runtime;
using System.IO;
using System.Windows.Forms;

namespace Uroboros.syntax.commands.core
{
    class Cut : CoreCommand
    {
        public Cut(IListable list)
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
                throw new CommandException("Action ignored! " + s + fileName + " is already cut.");
            }

            try
            {
                paths.Add(location);
                DataObject data = new DataObject();
                data.SetFileDropList(paths);
                data.SetData("Preferred DropEffect", DragDropEffects.Move);
                Clipboard.SetDataObject(data, true);

                RuntimeVariables.GetInstance().Success();
                Logger.GetInstance().LogCommand("Cut " + fileName);
            }
            catch (Exception)
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! Something went wrong during cutting " + fileName + ".");
            }
        }
    }
}
