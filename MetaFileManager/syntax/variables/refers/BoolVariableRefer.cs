using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.variables.refers
{
    class BoolVariableRefer : IBoolable
    {
        private string name;

        public BoolVariableRefer(string name)
        {
            this.name = name;
        }

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }

        public bool ToBool()
        {
            return RuntimeVariables.GetInstance().GetValueBool(name);
        }

        public decimal ToNumber()
        {
            if (ToBool())
                return 1;
            else
                return 0;
        }

        public override string ToString()
        {
            if (ToBool())
                return "true";
            else
                return "false";
        }

    }
}
