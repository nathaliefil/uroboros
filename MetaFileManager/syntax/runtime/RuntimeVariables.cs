using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;

namespace DivineScript.syntax.runtime
{
    public partial class RuntimeVariables
    {
        private static RuntimeVariables INSTANCE = new RuntimeVariables();
        private List<NamedVariable> variables;

        private RuntimeVariables()
        {
            InitializeInnerVariables();
        }

        public static RuntimeVariables GetInstance()
        {
            return INSTANCE;
        }

        public void BracketsUp()
        {
            foreach (NamedVariable var in variables)
            {
                var.BracketsUp();
            }
        }

        public void BracketsDown()
        {
            foreach (NamedVariable var in variables)
            {
                var.BracketsDown();
                if (var.NegativeDepth())
                    variables.Remove(var);
            }
        }

        public string GetValueString(string name)
        {
            NamedVariable nv = variables.First(v => name.Equals(v.GetName()));
            if (!nv.Equals(null))
            {
                return nv.ToString();
            }
            return "";
        }

        public decimal GetValueNumber(string name)
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

        public bool GetValueBool(string name)
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


        public void InitializeInnerVariables()
        {
            variables = new List<NamedVariable>();

            ListVariable empty = new ListVariable("empty",new List<string>());
            empty.SetConstant();

            variables.Add(empty);
            variables.Add(new Files());
            variables.Add(new Directories());
            variables.Add(new Everything());

            variables.Add(new StringVariable("location", ""));
            variables.Add(new StringVariable("name", ""));
            variables.Add(new StringVariable("fullname", ""));
            variables.Add(new StringVariable("extension", ""));

            variables.Add(new NumericVariable("index", 0));

        }

    }
}
