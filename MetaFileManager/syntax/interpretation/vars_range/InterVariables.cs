using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.interpretation.vars_range
{
    public class InterVariables
    {
        /*
         
        this is a simplified model of runtime variables
        it is used to determine during interpretation,
        if variables in expressions are declared
        and if they are in block range

        */


        private static InterVariables INSTANCE = new InterVariables();
        private List<InterVar> variables;

        private InterVariables()
        {
            Clear();
        }

        public static InterVariables GetInstance()
        {
            return INSTANCE;
        }

        public void BracketsUp()
        {
            foreach (InterVar var in variables)
            {
                var.BracketsUp();
            }
        }

        public void BracketsDown()
        {
            foreach (InterVar var in variables)
            {
                var.BracketsDown();
                if (var.NegativeDepth())
                    variables.Remove(var);
            }
        }

        public void Clear()
        {
            variables = new List<InterVar>();

            variables.Add(new InterVar("files", InterVarType.List, false));
            variables.Add(new InterVar("directories", InterVarType.List, false));
            variables.Add(new InterVar("everything", InterVarType.List, false));

            variables.Add(new InterVar("true", InterVarType.Bool, false));
            variables.Add(new InterVar("false", InterVarType.Bool, false));
            variables.Add(new InterVar("empty", InterVarType.Bool, false));

            variables.Add(new InterVar("this", InterVarType.String, true));
            variables.Add(new InterVar("location", InterVarType.String, true));
            variables.Add(new InterVar("index", InterVarType.Number, true));

            variables.Add(new InterVar("name", InterVarType.String, false));
            variables.Add(new InterVar("fullname", InterVarType.String, false));
            variables.Add(new InterVar("extension", InterVarType.String, false));
        }

        public bool Contains(string name, InterVarType type)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return false;

            InterVar iv = variables.First(v => name.Equals(v.GetName()));

            switch (type)
            {
                case InterVarType.Bool:
                {
                    return iv.IsBool() ? true : false;
                }
                case InterVarType.Number:
                {
                    return iv.IsNumber() ? true : false;
                }
                case InterVarType.String:
                {
                    return iv.IsString() ? true : false;
                }
                case InterVarType.List:
                {
                    return iv.IsList() ? true : false;
                }
            }
            return false;
        }

        public bool ContainsChangable(string name, InterVarType type)
        {
            if (!Contains(name, type))
                return false;

            InterVar iv = variables.First(v => name.Equals(v.GetName()));

            if (iv.IsChangeable())
                return true;
            return false;
        }
    }
}
