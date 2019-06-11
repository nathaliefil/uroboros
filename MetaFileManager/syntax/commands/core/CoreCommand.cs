using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.commands.core
{
    abstract class CoreCommand: ICommand
    {
        private IListable list;
        private BoolExpression where;


        public void Run()
        {
            List<string> elements = list.ToList();
            foreach (string element in elements)
            {
                PerformAction(element);
            }
        }

        private void PerformAction(string element)
        {
        }
    }

}
