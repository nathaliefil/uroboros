using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.variables.from_location;
using Uroboros.syntax.variables.bools;

namespace Uroboros.syntax.runtime
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
            bool modified = false;
            List<NamedVariable> copy = new List<NamedVariable>(variables.ToArray());

            foreach (NamedVariable var in variables)
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

        public List<string> GetValueList(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return new List<string>();

            NamedVariable nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is IListable)
            {
                return (nv as IListable).ToList();
            }
            return new List<string>();
        }

        public string GetValueString(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return "";

            NamedVariable nv = variables.First(v => name.Equals(v.GetName()));
            return nv.ToString();
        }

        public decimal GetValueNumber(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return 0;

            NamedVariable nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is INumerable)
            {
                return (nv as INumerable).ToNumber();
            }
            return 0;
        }

        public bool GetValueBool(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return false;

            NamedVariable nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is IBoolable)
            {
                return (nv as IBoolable).ToBool();
            }
            return false;
        }


        public void InitializeInnerVariables()
        {
            variables = new List<NamedVariable>();

            variables.Add(new Files());
            variables.Add(new Directories());
            variables.Add(new Everything());

            variables.Add(new True());
            variables.Add(new False());
            variables.Add(new Empty());

            variables.Add(new StringVariable("this", ""));
            variables.Add(new StringVariable("location", ""));
            variables.Add(new NumericVariable("index", 0));

            variables.Add(new Name());
            variables.Add(new Fullname());
            variables.Add(new Extension());
        }

    }
}
