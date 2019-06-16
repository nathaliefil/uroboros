using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.commands.list
{
    class Add : ICommand
    {
        private string name;
        private IStringable value;
        private IListable values;
        private bool single;

        public Add(string name, IStringable value)
        {
            this.name = name;
            this.value = value;
            single = true;
        }

        public Add(string name, IListable values)
        {
            this.name = name;
            this.values = values;
            single = false;
        }


        public void Run()
        {
            if (single)
                RuntimeVariables.GetInstance().Add(name, value.ToString());
            else
                RuntimeVariables.GetInstance().Add(name, values.ToList());
        }
    }
}
