using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.commands.core
{
    class CoreCommandCreate : CoreCommand
    {
        protected bool forced;

        public override void Action(string element)
        {
            if (FileValidator.IsNameCorrect(element))
            {
                string rawLocation = RuntimeVariables.GetInstance().GetWholeLocation();
                string location = rawLocation + "//" + element;

                if (FileValidator.IsDirectory(element))
                {
                    if (Directory.Exists(location))
                    {
                        if (forced)
                            Directory.Delete(@location, true);
                        else
                            throw new CommandException("Action ignored! Directory " + element + " already exists and thus cannot be created.");
                    }
                    DirectoryAction(element, location);
                }
                else
                {
                    if (File.Exists(location))
                    {
                        if (forced)
                            File.Delete(@location);
                        else
                            throw new CommandException("Action ignored! File " + element + " already exists and thus cannot be created.");
                    }
                    FileAction(element, location);
                }
            }
            else
                throw new CommandException("Action ignored! " + element + " contains not allowed characters.");
        }
    }
}
