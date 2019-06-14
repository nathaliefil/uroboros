using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;
using System.IO;

namespace DivineScript.syntax.commands.create
{
    class CreateDirectory : ICommand
    {
        ListExpression list;

        public CreateDirectory(ListExpression list)
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
                    if (FileValidator.IsDirectory(name))
                    {
                        if (Directory.Exists(@location))
                        {
                            Logger.GetInstance().Log("Error! Directory " + name + " already exists.");
                        }
                        else
                        {
                            try
                            {
                                Directory.CreateDirectory(@location);
                                Logger.GetInstance().Log("Create directory " + name);
                            }
                            catch (Exception)
                            {
                                Logger.GetInstance().Log("Error! Something went wrong during creating " + name + ".");
                            }
                        }
                    }
                    else
                    {
                        Logger.GetInstance().Log("Error! " + name + " is not a directory.");
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
