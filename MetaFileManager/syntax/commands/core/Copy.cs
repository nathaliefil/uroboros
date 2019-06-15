using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;
using System.IO;
using System.Collections.Specialized;
using System.Windows.Forms;
using DivineScript.syntax.variables.expressions.list;

namespace DivineScript.syntax.commands.core
{
    class Copy : CoreCommand
    {
        public Copy(ListExpression list)
        {
            this.list = list;
        }

        public override void Run()
        {
            StringCollection paths = new StringCollection();
            List<string> elements = list.ToList();
            foreach (string element in elements)
            {
                if (FileValidator.IsNameCorrect(element))
                {
                    string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + element;

                    if (FileValidator.IsDirectory(element))
                    {
                        if (!Directory.Exists(@location))
                        {
                            Logger.GetInstance().Log("Action ignored! Directory " + element + " not found.");
                        }
                        else
                        {
                            paths.Add(location);
                            Logger.GetInstance().Log("Copy " + element + " to clipboard.");
                        }
                    }
                    else
                    {
                        if (!File.Exists(@location))
                        {
                            Logger.GetInstance().Log("Action ignored! File " + element + " not found.");
                        }
                        else
                        {
                            paths.Add(location);
                            Logger.GetInstance().Log("Copy " + element + " to clipboard.");
                        }
                    }
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! " + element + " contains not allowed characters.");
                }
            }

            Clipboard.SetFileDropList(paths);
        }
    }
}
