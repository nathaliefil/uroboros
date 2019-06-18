using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.commands.core
{
    class Drop : CoreCommand
    {
        public Drop(IListable list)
        {
            this.list = list;
        }

        protected override void PerformDirectoryAction(string element, string location)
        {
            try
            {
                Directory.Delete(@location);
                Logger.GetInstance().Log("Delete " + element);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                {
                    Logger.GetInstance().Log("Action ignored! Access denied to " + element + ".");
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! Something went wrong during dropping " + element + ".");
                }
            }
        }

        protected override void PerformFileAction(string element, string location)
        {
            try
            {
                File.Delete(@location);
                Logger.GetInstance().Log("Delete " + element);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                {
                    Logger.GetInstance().Log("Action ignored! Access denied to " + element + ".");
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! Something went wrong during dropping " + element + ".");
                }
            }
        }
    }
}
