using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.commands.arithmetic
{
    class DivideBy : ICommand
    {
        string variable;
        NumericExpression value;

        public DivideBy(string variable, NumericExpression value)
        {
            this.variable = variable;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().DivideBy(variable, value.ToNumber());
        }
    }
}
