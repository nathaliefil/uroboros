using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;
using System.IO;

namespace DivineScript.syntax.commands.create
{
    class CreateFileFrom : ICommand
    {
        private StringExpression name;
        private StringExpression source;

        public CreateFileFrom(StringExpression source, StringExpression name)
        {
            this.name = name;
            this.source = source;
        }

        public void Run()
        {
            string sname = source.ToString();
            string nname = name.ToString();

            if (!FileValidator.IsNameCorrect(sname))
            {
                Logger.GetInstance().Log("Error! " + sname + " contains not allowed characters.");
                return;
            }
            if (!FileValidator.IsNameCorrect(nname))
            {
                Logger.GetInstance().Log("Error! " + nname + " contains not allowed characters.");
                return;
            }
            if (FileValidator.IsDirectory(sname))
            {
                Logger.GetInstance().Log("Error! " + sname + " is not a file.");
                return;
            }
            if (FileValidator.IsDirectory(nname))
            {
                Logger.GetInstance().Log("Error! " + nname + " is not a file.");
                return;
            }
            string slocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + sname;
            string nlocation = RuntimeVariables.GetInstance().GetValueString("location") + "//" + nname;
            if (!File.Exists(@slocation))
            {
                Logger.GetInstance().Log("Error! Source file " + sname + " do not exist.");
                return;
            }
            if (File.Exists(@nlocation))
            {
                Logger.GetInstance().Log("Error! File " + nname + " already exists.");
                return;
            }

            try
            {
                File.Copy(@slocation, @nlocation);
                Logger.GetInstance().Log("Create " + nname + " from " + sname);
            }
            catch (Exception)
            {
                Logger.GetInstance().Log("Error! Something went wrong during creating " + nname + " from " + nname + ".");
            }
        }
    }
}
