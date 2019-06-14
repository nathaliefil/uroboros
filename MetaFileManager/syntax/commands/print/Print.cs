using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.expressions;
using DivineScript.syntax.runtime;

namespace DivineScript.syntax.commands.print
{
    class Print : ICommand
    {
        private StringExpression text;

        public Print(StringExpression text)
        {
            this.text = text;
        }

        public void Run()
        {
            Logger.GetInstance().Log(text.ToString());
        }
    }
}
