using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.variables.abstracts
{
    abstract class NamedVariable : DefaultToListMethod
	{
        protected string name = "";
        protected int depthInBrackets = 0;

        public string GetName()
        {
            return name;
        }

        public int GetDepthInBrackets()
        {
            return depthInBrackets;
        }

        public void BracketsUp()
        {
            depthInBrackets++;
        }

        public void BracketsDown()
        {
            depthInBrackets--;
        }

        public bool NegativeDepth()
        {
            return depthInBrackets<0? true: false;
        }
	}
}
