using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using System.IO;

namespace DivineScript.syntax.commands.create
{
    class CreateDirectory : ICommand
    {
        private IStringable name;
        private bool forced;

        public CreateDirectory(IStringable name, bool forced)
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
                if (FileValidator.IsDirectory(sname))
                {
                    if (Directory.Exists(@location))
                    {
                        if (!forced)
                        {
                            Logger.GetInstance().Log("Action ignored! Directory " + sname + " already exists.");
                        }
                        else 
                        {
                            try
                            {
                                Directory.Delete(@location);
                                Directory.CreateDirectory(@location);
                                Logger.GetInstance().Log("Create directory " + sname + " replacing existing one");
                            }
                            catch (Exception)
                            {
                                Logger.GetInstance().Log("Action ignored! Something went wrong during replacing existing directory " + sname + ".");
                            }
                        }
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
