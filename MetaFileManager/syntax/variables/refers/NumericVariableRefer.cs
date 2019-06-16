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
            decimal value = ToNumber();

            if (value % 1 == 0)
                return ((int)value).ToString();
            else
                return value.ToString();
        }
    }
}
