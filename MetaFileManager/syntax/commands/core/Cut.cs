using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using System.Collections.Specialized;
using DivineScript.syntax.runtime;
using System.IO;
using System.Windows.Forms;

namespace DivineScript.syntax.commands.core
{
    class Cut : CoreCommand
    {
        public Cut(IListable list)
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
                            Logger.GetInstance().Log("Cut " + element + " to clipboard.");
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
                            Logger.GetInstance().Log("Cut " + element + " to clipboard.");
                        }
                    }
                }
                else
                {
                    Logger.GetInstance().Log("Action ignored! " + element + " contains not allowed characters.");
                }
            }

            Clipboard.Clear();
            if (paths.Count > 0)
            {
                DataObject data = new DataObject();
                data.SetData("Preferred DropEffect", DragDropEffects.Move);
                Clipboard.SetDataObject(data);
            }
        }

    }
}
