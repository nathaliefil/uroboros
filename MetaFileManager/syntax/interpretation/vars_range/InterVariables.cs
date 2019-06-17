using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.interpretation.vars_range
{
    public class InterVariables
    {
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
    }
}
