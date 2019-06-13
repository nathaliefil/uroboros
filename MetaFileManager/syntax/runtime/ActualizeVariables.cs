using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.runtime
{
    public partial class RuntimeVariables
    {

        public void Actualize(string name, string value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 0)
            {
                variables.Add(new StringVariable(name, value));
            }
            else if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is StringVariable)
                    (nv as StringVariable).SetValue(value);
            }
        }

        public void Actualize(string name, decimal value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 0)
            {
                variables.Add(new NumericVariable(name, value));
            }
            else if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).SetValue(value);
            }
        }

        public void Actualize(string name, bool value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 0)
            {
                variables.Add(new BoolVariable(name, value));
            }
            else if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is BoolVariable)
                    (nv as BoolVariable).SetValue(value);
            }
        }

        public void Actualize(string name, List<string> value)
        {
            /*
             * 
             *  to do
             * 
             * */
        }

        public void PlusPlus(string name)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).PlusPlus();
            }
        }

        public void MinusMinus(string name)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).MinusMinus();
            }
        }

        public void MultiplyBy(string name, decimal value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).MultiplyBy(value);
            }
        }

        public void DivideBy(string name, decimal value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                NamedVariable nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).DivideBy(value);
            }
        }

    }
}
