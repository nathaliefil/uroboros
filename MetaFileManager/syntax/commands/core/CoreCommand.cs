using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    abstract class CoreCommand: ICommand
    {
        protected IListable list;

        public void Run()
        {
            List<string> elements = list.ToList();
            foreach (string element in elements)
            {
                try
                {
                    Action(element);
                }
                catch (CommandException ce)
                {
                    Logger.GetInstance().LogCommandError(ce.GetMessage());
                }
            }
        }

        public virtual void Action(string element)
        {
            if (FileValidator.IsNameCorrect(element))
            {
                string rawLocation = RuntimeVariables.GetInstance().GetWholeLocation();
                string location = rawLocation + "//" + element;

                if (FileValidator.IsDirectory(element))
                {
                    if (!Directory.Exists(@location))
                    {
                        RuntimeVariables.GetInstance().Failure();
                        throw new CommandException("Action ignored! Directory " + element + " not found.");
                    }
                    else
                        DirectoryAction(element, rawLocation);
                }
                else
                {
                    if (!File.Exists(@location))
                    {
                        RuntimeVariables.GetInstance().Failure();
                        throw new CommandException("Action ignored! File " + element + " not found.");
                    }
                    else
                        FileAction(element, rawLocation);
                }
            }
            else
            {
                RuntimeVariables.GetInstance().Failure();
                throw new CommandException("Action ignored! " + element + " contains not allowed characters.");
            }
        }

        protected virtual void DirectoryAction(string element, string location)
        {
            // to be overridden
            // but not necessarily
        }

        protected virtual void FileAction(string element, string location)
        {
            // to be overridden
            // but not necessarily
        }
    }
}
