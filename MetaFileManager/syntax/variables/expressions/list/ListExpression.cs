using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.expressions
{
    class ListExpression: Variable, IListable
    {
        private List<string> elements;
        private BoolExpression where;

        public List<string> ToList()
        {
            return elements;
        }

        public ListExpression(List<string> elements)
        {
            this.elements = elements;
        }




        /*''
         * 
         *  todo everything
         * 
         * 
         * */

        
    }
}
