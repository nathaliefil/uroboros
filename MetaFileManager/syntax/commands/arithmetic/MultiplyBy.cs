using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.runtime;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.commands.arithmetic
{
    class MultiplyBy : ICommand
    {
        string variable;
        NumericExpression value;

        public MultiplyBy(string variable, NumericExpression value)
        {
            this.variable = variable;
            this.value = value;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().MultiplyBy(variable, value.ToNumber());
        }
    }
}
