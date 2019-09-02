using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;
using Uroboros.syntax.log;

namespace Uroboros.syntax.commands.core
{
    abstract class CoreCommand: ICommand
    {
        protected IListable list;

        public void Run()
        {
            if (list is IStringable)
                RunOneFile();
            else
                RunListOfFiles();
        }

        public void RunOneFile()
        {
            string previousThis = RuntimeVariables.GetInstance().GetValueString("this");
            string element = (list as IStringable).ToString();

            try
            {
                Action(element);
            }
            catch (CommandException ce)
            {
                Logger.GetInstance().LogCommandError(ce.GetMessage());
            }

            RuntimeVariables.GetInstance().Actualize("this", previousThis);
        }

        public void RunListOfFiles()
        {
            decimal previousIndex = RuntimeVariables.GetInstance().GetValueNumber("index");
            string previousThis = RuntimeVariables.GetInstance().GetValueString("this");
            RuntimeVariables.GetInstance().Actualize("index", 0);

            List<string> elements = list.ToList();
            foreach (string element in elements)
            {
                RuntimeVariables.GetInstance().Actualize("this", element);

                try
                {
                    Action(element);
                }
                catch (CommandException ce)
                {
                    Logger.GetInstance().LogCommandError(ce.GetMessage());
                }

                RuntimeVariables.GetInstance().IncrementBy("index", 1);
            }

            RuntimeVariables.GetInstance().Actualize("index", previousIndex);
            RuntimeVariables.GetInstance().Actualize("this", previousThis);
        }

        public virtual void Action(string element)
        {
            if (element.Trim().Equals(""))
                throw new CommandException("Action ignored! Impossible to perform action on empty element.");

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
