using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.variables.refers
{
    class NumericVariableRefer : INumerable
    {
        private string name;

        public NumericVariableRefer(string name)
        {
            this.name = name;
        }

        public decimal ToNumber()
        {
            return RuntimeVariables.GetInstance().GetValueNumber(name);
        }

        public override string ToString()
        {
            return ToNumber().ToString();
        }

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
