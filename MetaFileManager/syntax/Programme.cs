using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.commands;
using DivineScript.syntax.reading;

namespace DivineScript.syntax
{
    class Programme
    {
        private string name;
        List <ICommand> commands;

        public Programme(string name, string code)
        {
            this.name = name;

            BuildCommandList(code);
        }

        private string GetName()
        {
            return name;
        }

        private void BuildCommandList(string code)
        {
            commands = new List<ICommand>();

            

        }
    }
}
