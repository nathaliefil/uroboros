using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.list
{
    class Remove : ICommand
    {
        private string name;
        private string value;
        private List<string> values;
        private bool single;

        public Remove(string name, string value)
        {
            this.name = name;
            this.value = value;
            single = true;
        }

        public Remove(string name, List<string> values)
        {
            this.name = name;
            this.values = values;
            single = false;
        }


        public void Run()
        {
            if (single)
                RuntimeVariables.GetInstance().Remove(name, value);
            else
                RuntimeVariables.GetInstance().Remove(name, values);
        }
    }
}