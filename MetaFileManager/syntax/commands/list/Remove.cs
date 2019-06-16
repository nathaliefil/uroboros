using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.commands.list
{
    class Remove : ICommand
    {
        private string name;
        private IStringable value;
        private IListable values;
        private bool single;

        public Remove(string name, IStringable value)
        {
            this.name = name;
            this.value = value;
            single = true;
        }

        public Remove(string name, IListable values)
        {
            this.name = name;
            this.values = values;
            single = false;
        }


        public void Run()
        {
            if (single)
                RuntimeVariables.GetInstance().Remove(name, value.ToString());
            else
                RuntimeVariables.GetInstance().Remove(name, values.ToList());
        }
    }
}
