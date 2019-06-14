using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using System.IO;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.core
{
    class Drop : CoreCommand
    {
        public Drop(ListExpression list)
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
                    Logger.GetInstance().Log("Error! Access denied to " + element + ".");
                }
                else
                {
                    Logger.GetInstance().Log("Error! Unknown problem occured to " + element + ".");
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
                    Logger.GetInstance().Log("Error! Access denied to " + element + ".");
                }
                else
                {
                    Logger.GetInstance().Log("Error! Unknown problem occured to " + element + ".");
                }
            }
        }
    }
}
