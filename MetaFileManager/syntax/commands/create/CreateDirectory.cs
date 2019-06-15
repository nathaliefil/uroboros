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
        private StringExpression name;

        public CreateDirectory(StringExpression name)
        {
            this.name = name;
        }

        public void Run()
        {
            string sname = name.ToString();
            if (FileValidator.IsNameCorrect(sname))
            {
                string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + sname;
                if (FileValidator.IsDirectory(sname))
                {
                    if (Directory.Exists(@location))
                    {
                        Logger.GetInstance().Log("Action ignored! Directory " + sname + " already exists.");
                    }
                    else
                    {
                        try
                        {
                            Directory.CreateDirectory(@location);
                            Logger.GetInstance().Log("Create directory " + sname);
                        }
                        catch (Exception)
                        {
                            Logger.GetInstance().Log("Action ignored! Something went wrong during creating " + sname + ".");
                        }
                    }
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! " + sname + " is not a directory.");
                }
            }
            else
            {
                Logger.GetInstance().Log("Action ignored! " + sname + " contains not allowed characters.");
            }
        }
    }
}
