using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.runtime
{
    static class RuntimeVariables
    {
        private static List<NamedVariable> variables;

        static RuntimeVariables()
        {
            variables = new List<NamedVariable>();
            InitializeInnerVariables();
        }

        private static void InitializeInnerVariables()
        {
        }

        public static void Actualize(NamedVariable variable)
        {
            if (variables.Where(v => v.GetName().Equals(variable.GetName())).Count() == 0)
            {
                variables.Add(variable);
            }
            if (variables.Where(v => v.GetName().Equals(variable.GetName())).Count() == 1)
            {
                variables.Add(variable);
            }
        }

        public static void BracketsUp()
        {
            foreach (NamedVariable var in variables)
            {
                var.BracketsUp();
            }
        }

        public static void BracketsDown()
        {
            foreach (NamedVariable var in variables)
            {
                var.BracketsDown();
                if (var.NegativeDepth())
                    variables.Remove(var);
            }
        }

        public static string GetValueString(string name)
        {
            NamedVariable nv = variables.First(v => name.Equals(v.GetName()));
            if (!nv.Equals(null))
            {
                return nv.ToString();
            }
            return "";
        }

        public static decimal GetValueNumber(string name)
        {
            NamedVariable nv = variables.First(v => name.Equals(v.GetName()));
            if (!nv.Equals(null))
            {
                if (nv is INumerable)
                {
                    return (nv as INumerable).ToNumber();
                }
            }
            return 0;
        }

        public static bool GetValueBool(string name)
        {
            NamedVariable nv = variables.First(v => name.Equals(v.GetName()));
            if (!nv.Equals(null))
            {
                if (nv is IBoolable)
                {
                    return (nv as IBoolable).ToBool();
                }
            }
            return false;
        }

    }
}
