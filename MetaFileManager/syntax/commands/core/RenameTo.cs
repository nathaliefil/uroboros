﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class RenameTo : CoreCommand
    {
        private IStringable destination;
        private bool forced;

        public RenameTo(IListable list, IStringable destination, bool forced)
        {
            this.list = list;
            this.destination = destination;
            this.forced = forced;
        }

        protected override void PerformFileAction(string element, string t)
        {
            string sname = element;
            string nname = destination.ToString();

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

            string slocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + sname;
            string nlocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + nname;

            try
            {
                if (forced)
                    if (File.Exists(nlocation))
                        File.Delete(@nlocation);
                File.Move(@slocation, @nlocation);
                Logger.GetInstance().Log("Rename " + sname + " to " + nname);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                {
                    Logger.GetInstance().Log("Action ignored! Access denied during renaming " + sname + " to " + nname + ".");
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! Something went wrong during renaming " + sname + " to " + nname + ".");
                }
            }
        }

        protected override void PerformDirectoryAction(string element, string t)
        {
            string sname = element;
            string nname = destination.ToString();

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

            string slocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + sname;
            string nlocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + nname;

            try
            {
                if (forced)
                    if (Directory.Exists(nlocation))
                        Directory.Delete(@nlocation);
                Directory.Move(@slocation, @nlocation);
                Logger.GetInstance().Log("Rename " + sname + " to " + nname);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                {
                    Logger.GetInstance().Log("Action ignored! Access denied during renaming " + sname + " to " + nname + ".");
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! Something went wrong during renaming " + sname + " to " + nname + ".");
                }
            }
        }
    }
}
