using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.expressions
{
    class ListExpression: Variable, IListable
    {
        List<string> elements;

        /*''
         * 
         *  todo everything
         * 
         * 
         * */

        public List<string> ToList()
        {
            return elements;
        }
    }
}
