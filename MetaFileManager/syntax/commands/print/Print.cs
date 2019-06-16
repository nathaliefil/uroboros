using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.print
{
    class Print : ICommand
    {
        private IStringable text;

        public Print(IStringable text)
        {
            this.text = text;
        }

        public void Run()
        {
            Logger.GetInstance().Log(text.ToString());
        }
    }
}
