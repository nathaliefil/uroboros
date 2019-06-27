using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncLetter : DefaultStringable
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
    }
}
