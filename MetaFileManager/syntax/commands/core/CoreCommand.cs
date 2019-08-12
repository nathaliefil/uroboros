﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    abstract class CoreCommand: DefaultBoolable, ICommand
    {
        protected IListable list;

        public void Run()
        {
            foreach (string element in list.ToList())
            {
                try
                {
                    Action(element);
                }
                catch (CommandException ce)
                {
                    Logger.GetInstance().LogCommand(ce.GetMessage());
                }
            }
        }

        public virtual void Action(string element)
        {
            if (FileValidator.IsNameCorrect(element))
            {
                string rawLocation = RuntimeVariables.GetInstance().GetLocation();
                string location = rawLocation + "//" + element;

                if (FileValidator.IsDirectory(element))
                {
                    if (!Directory.Exists(@location))
                        throw new CommandException("Action ignored! Directory " + element + " not found.");
                    else
                        DirectoryAction(element, rawLocation);
                }
                else
                {
                    if (!File.Exists(@location))
                        throw new CommandException("Action ignored! File " + element + " not found.");
                    else
                        FileAction(element, rawLocation);
                }
            }
            else
                throw new CommandException("Action ignored! " + element + " contains not allowed characters.");
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

        public override bool ToBool()
        {
            try
            {
                Run();
            }
            catch (CommandException)
            {
                return false;
            }
            return true;
        }
    }
}
