using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.commands.var
{
    class Swap : ICommand
    {
        string leftName;
        string rightName;

        public Swap(string leftName, string rightName)
        {
            this.leftName = leftName;
            this.rightName = rightName;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Swap(leftName, rightName);
        }
    }
}
