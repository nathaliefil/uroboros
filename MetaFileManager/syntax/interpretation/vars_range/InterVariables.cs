using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.interpretation.vars_range
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

        public void TurnToList(string name)
        {
            InterVar iv = variables.First(v => v.GetName().Equals(name));
            iv.TurnToList();
        }

        public void Add(string name, InterVarType type)
        {
            variables.Add(new InterVar(name, type, true));
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
            bool modified = false;
            List<InterVar> copy = new List<InterVar>(variables.ToArray());

            foreach (InterVar var in variables)
            {
                var.BracketsDown();
                if (var.NegativeDepth())
                {
                    modified = true;
                    copy.Remove(var);
                }
            }
            if (modified)
                variables = copy;
        }

        public void Clear()
        {
            variables = new List<InterVar>();

            variables.Add(new InterVar("files", InterVarType.List, false));
            variables.Add(new InterVar("directories", InterVarType.List, false));
            variables.Add(new InterVar("everything", InterVarType.List, false));

            variables.Add(new InterVar("this", InterVarType.String, true));
            variables.Add(new InterVar("path", InterVarType.String, false));
            variables.Add(new InterVar("location", InterVarType.String, true));
            variables.Add(new InterVar("index", InterVarType.Number, true));

            variables.Add(new InterVar("name", InterVarType.String, false));
            variables.Add(new InterVar("fullname", InterVarType.String, false));
            variables.Add(new InterVar("extension", InterVarType.String, false));
            variables.Add(new InterVar("exist", InterVarType.Bool, false));
            variables.Add(new InterVar("empty", InterVarType.Bool, false));
            variables.Add(new InterVar("access", InterVarType.Time, false));
            variables.Add(new InterVar("creation", InterVarType.Time, false));
            variables.Add(new InterVar("modification", InterVarType.Time, false));
            variables.Add(new InterVar("now", InterVarType.Time, false));
            variables.Add(new InterVar("size", InterVarType.Number, false));
            variables.Add(new InterVar("isfile", InterVarType.Bool, false));
            variables.Add(new InterVar("isdirectory", InterVarType.Bool, false));
            variables.Add(new InterVar("iscorrect", InterVarType.Bool, false));
        }

        public bool Contains(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return false;
            return true;
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
                case InterVarType.Time:
                {
                    return iv.IsTime() ? true : false;
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

        public InterVar GetVar(string name)
        {
            InterVar result = variables.Where(v => v.GetName().Equals(name)).First();
            return result;
        }

        public InterVarType GetVarType(string name)
        {
            InterVar result = variables.Where(v => v.GetName().Equals(name)).First();
            return result.GetVarType();
        }
    }
}
