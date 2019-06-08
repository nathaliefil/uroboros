using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.variables.abstracts
{
	abstract class NamedVariable: Variable
	{
        protected string name;

        public string GetName()
        {
            return name;
        }
	}
}
