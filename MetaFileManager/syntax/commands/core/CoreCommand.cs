using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using System.IO;

namespace DivineScript.syntax.commands.core
{
    abstract class CoreCommand: ICommand
    {
        protected IListable list;


        public virtual void Run()
        {
            List<string> elements = list.ToList();
            foreach (string element in elements)
            {
                if (FileValidator.IsNameCorrect(element))
                {
                    string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + element;

                    if (FileValidator.IsDirectory(element))
                    {
                        if (!Directory.Exists(@location))
                        {
                            Logger.GetInstance().Log("Action ignored! Directory " + element + " not found.");
                        }
                        else
                        {
                            PerformDirectoryAction(element, location);
                        }
                    }
                    else
                    {
                        if (!File.Exists(@location))
                        {
                            Logger.GetInstance().Log("Action ignored! File " + element + " not found.");
                        }
                        else
                        {
                            PerformFileAction(element, location);
                        }
                    }
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! " + element + " contains not allowed characters.");
                }
            }
        }

        protected virtual void PerformDirectoryAction(string element, string location)
        {
            // to be overridden
        }

        protected virtual void PerformFileAction(string element, string location)
        {
            // to be overridden
        }
    }

}
