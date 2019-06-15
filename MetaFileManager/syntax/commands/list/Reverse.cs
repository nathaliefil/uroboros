using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.list
{
    class Reverse : ICommand
    {
        private string name;

        public Reverse(string name)
        {
            this.name = name;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Reverse(name);
        }
    }
}
