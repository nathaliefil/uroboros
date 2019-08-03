using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class CreateDirectoryFrom : ICommand
    {
        private IStringable name;
        private IStringable source;
        private bool forced;

        public CreateDirectoryFrom(IStringable source, IStringable name, bool forced)
        {
            this.name = name;
            this.source = source;
            this.forced = forced;
        }

        public void Run()
        {
            string sname = source.ToString();
            string nname = name.ToString();
            /// to do
            /// to do



            /*
            if (!FileValidator.IsNameCorrect(sname))
            {
                Logger.GetInstance().Log("Action ignored! " + sname + " contains not allowed characters.");
                return;
            }
            if (!FileValidator.IsNameCorrect(nname))
            {
                Logger.GetInstance().Log("Action ignored! " + nname + " contains not allowed characters.");
                return;
            }
            if (FileValidator.IsDirectory(sname))
            {
                Logger.GetInstance().Log("Action ignored! " + sname + " is not a file.");
                return;
            }
            if (FileValidator.IsDirectory(nname))
            {
                Logger.GetInstance().Log("Action ignored! " + nname + " is not a file.");
                return;
            }
            string slocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + sname;
            string nlocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + nname;
            if (!File.Exists(@slocation))
            {
                Logger.GetInstance().Log("Action ignored! Source file " + sname + " do not exist.");
                return;
            }
            if (File.Exists(@nlocation))
            {
                Logger.GetInstance().Log("Action ignored!! File " + nname + " already exists.");
                return;
            }

            try
            {
                File.Copy(@slocation, @nlocation);
                Logger.GetInstance().Log("Create " + nname + " from " + sname);
            }
            catch (Exception)
            {
                Logger.GetInstance().Log("Action ignored! Something went wrong during creating " + nname + " from " + nname + ".");
            }
             * 
             * */
        }
    }
}
