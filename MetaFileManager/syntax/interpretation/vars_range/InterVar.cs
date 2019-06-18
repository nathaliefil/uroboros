using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.interpretation.vars_range
{
    public class InterVar
    {
        private string name;
        private InterVarType type;
        private int depthInBrackets;
        private bool changeable;

        public InterVar(string name, InterVarType type, bool changeable)
        {
            this.name = name;
            this.type = type;
            this.changeable = changeable;
            depthInBrackets = 0;
        }

        public string GetName()
        {
            return name;
        }

        public InterVarType GetVarType()
        {
            return type;
        }

        public bool IsChangeable()
        {
            return changeable;
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
            return (depthInBrackets < 0 ? true : false);
        }

        public bool IsBool()
        {
            if (type.Equals(InterVarType.Bool))
                return true;
            else
                return false;
        }

        public bool IsNumber()
        {
            if (type.Equals(InterVarType.Bool) || type.Equals(InterVarType.Number))
                return true;
            else
                return false;
        }

        public bool IsString()
        {
            if (type.Equals(InterVarType.Bool) || type.Equals(InterVarType.Number)
                || type.Equals(InterVarType.String))
                return true;
            else
                return false;
        }

        public bool IsList()
        {
            if (type.Equals(InterVarType.Bool) || type.Equals(InterVarType.Number)
                || type.Equals(InterVarType.String) || type.Equals(InterVarType.List))
                return true;
            else
                return false;
        }
    }
}
