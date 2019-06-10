using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.variables.abstracts
{
    public abstract class Variable
    {
        private bool constant = false;

        public bool IsConstant()
        {
            return constant;
        }

    }
}
