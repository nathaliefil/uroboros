using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using System.IO;

namespace DivineScript.syntax.commands.create
{
    class CreateFile : ICommand
    {
        private IStringable name;
        private bool forced;

        public CreateFile(IStringable name, bool forced)
        {
            this.name = name;
            this.forced = forced;
        }

        public void Run()
        {
            string sname = name.ToString();
            if (FileValidator.IsNameCorrect(sname))
            {
                string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + sname;
                if (!FileValidator.IsDirectory(sname))
                {
                    if (File.Exists(@location))
                    {
                        Logger.GetInstance().Log("Action ignored! File " + sname + " already exists.");
                    }
                    else
                    {
                        try
                        {
                            File.Create(@location);
                            Logger.GetInstance().Log("Create file " + sname);
                        }
                        catch (Exception)
                        {
                            Logger.GetInstance().Log("Action ignored! Something went wrong during creating " + sname + ".");
                        }
                    }
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! " + sname + " is not a file.");
                }
            }
            else
            {
                Logger.GetInstance().Log("Action ignored! " + sname + " contains not allowed characters.");
            }
        }
        

    }
}
