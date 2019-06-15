using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.list
{
    class Add : ICommand
    {
        private string name;
        private string value;
        private List<string> values;
        private bool single;

        public Add(string name, string value)
        {
            this.name = name;
            this.value = value;
            single = true;
        }

        public Add(string name, List<string> values)
        {
            this.name = name;
            this.values = values;
            single = false;
        }


        public void Run()
        {
            if (single)
                RuntimeVariables.GetInstance().Add(name, value);
            else
                RuntimeVariables.GetInstance().Add(name, values);
        }
    }
}
