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
        public void Reverse(string name)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is ListVariable)
                    (nv as ListVariable).Reverse();
            }
        }

        public void Add(string name, string value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is ListVariable)
                    (nv as ListVariable).Add(value);
                if (nv is StringVariable)
                {
                    string val = (nv as StringVariable).ToString();
                    Remove(name);
                    List<string> lst = new List<string>();
                    lst.Add(val);
                    lst.Add(value);
                    Actualize(name, lst);
                }
            }
        }

        public void Add(string name, List<string> values)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is ListVariable)
                    (nv as ListVariable).Add(values);
                if (nv is StringVariable)
                {
                    string val = (nv as StringVariable).ToString();
                    Remove(name);
                    List<string> lst = new List<string>();
                    lst.Add(val);
                    lst.AddRange(values);
                    Actualize(name, lst);
                }
            }
        }

        public void Remove(string name, string value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is ListVariable)
                    (nv as ListVariable).Remove(value);
            }
        }

        public void Remove(string name, List<string> values)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is ListVariable)
                    (nv as ListVariable).Remove(values);
            }
        }
    }
}
