using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.functions.numeric
{
    class FuncLetter : IStringFunction
    {
        private NumericExpression arg0;

        public FuncLetter(NumericExpression arg0)
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
