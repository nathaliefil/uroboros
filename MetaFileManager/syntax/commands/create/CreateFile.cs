using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;
using System.IO;

namespace DivineScript.syntax.commands.create
{
    class CreateFile : ICommand
    {
        ListExpression list;

        public CreateFile(ListExpression list)
        {
            this.list = list;
        }

        public void Run()
        {
            foreach (string name in list.ToList())
            {
                if (FileValidator.IsNameCorrect(name))
                {
                    string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + name;
                    if (!FileValidator.IsDirectory(name))
                    {
                        if (File.Exists(@location))
                        {
                            Logger.GetInstance().Log("Error! File " + name + " already exists.");
                        }
                        else
                        {
                            try
                            {
                                File.Create(@location);
                                Logger.GetInstance().Log("Create file " + name);
                            }
                            catch (Exception)
                            {
                                Logger.GetInstance().Log("Error! Something went wrong during creating " + name + ".");
                            }
                        }
                    }
                    else
                    {
                        Logger.GetInstance().Log("Error! " + name + " is not a file.");
                    }
                }
                else
                {
                    Logger.GetInstance().Log("Error! " + name + " contains not allowed characters.");
                }
            }
        }

    }
}
