using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.functions.numeric
{
    class FuncLetter : IStringFunction
    {
        private INumerable arg0;

        public FuncLetter(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            int number = (int)arg0.ToNumber();
            StringBuilder sb = new StringBuilder();
            /*do
            {
                

            } while (number > 26);*/
            
            /// todo
            return "";
        }

        public List<string> ToList()
        {
            return new List<string> { ToString() };
        }
    }
}
