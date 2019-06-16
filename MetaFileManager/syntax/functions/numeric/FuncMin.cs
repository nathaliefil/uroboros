using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncMin : INumericFunction
    {
        private List<INumerable> arg0;

        public FuncMin(List<INumerable> arg0)
        {
            this.arg0 = arg0;
        }

        public decimal ToNumber()
        {
            return arg0.Min(x => x.ToNumber()); 
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            if (value % 1 == 0)
                return ((int)value).ToString();
            else
                return value.ToString();
        }

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
