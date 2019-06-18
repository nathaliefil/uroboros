using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables.refers
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
