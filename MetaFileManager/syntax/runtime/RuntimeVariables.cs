using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;

namespace DivineScript.syntax.runtime
{
    static partial class RuntimeVariables
    {
        private static List<NamedVariable> variables;

        static RuntimeVariables()
        {
            variables = new List<NamedVariable>();
            InitializeInnerVariables();
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


        private static void InitializeInnerVariables()
        {
        }

    }
}
