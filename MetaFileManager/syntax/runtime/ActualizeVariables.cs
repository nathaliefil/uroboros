using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.runtime
{
    public partial class RuntimeVariables
    {
        public void Remove(string name)
        {
            variables.Remove(variables.Where(v => v.GetName().Equals(name)).First());
        }

        public void Actualize(string name, string value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 0)
            {
                variables.Add(new StringVariable(name, value));
            }
            else if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is StringVariable)
                    (nv as StringVariable).SetValue(value);
            }
        }

        public void Actualize(string name, decimal value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 0)
            {
                variables.Add(new NumericVariable(name, value));
                return;
            }
            else if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
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
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is BoolVariable)
                    (nv as BoolVariable).SetValue(value);
            }
        }

        public void Actualize(string name, List<string> value)
        {
            List<string> copy = value.Select(item => item+"").ToList();
            // not sure if this makes deep copy

            if (variables.Where(v => v.GetName().Equals(name)).Count() == 0)
            {
                variables.Add(new ListVariable(name, copy));
            }
            else if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is ListVariable)
                    (nv as ListVariable).SetValue(copy);
            }
        }

        public void PlusPlus(string name)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).PlusPlus();
            }
        }

        public void MinusMinus(string name)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).MinusMinus();
            }
        }

        public void IncrementBy(string name, decimal value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).IncrementBy(value);
            }
        }

        public void DecrementBy(string name, decimal value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).DecrementBy(value);
            }
        }

        public void MultiplyBy(string name, decimal value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).MultiplyBy(value);
            }
        }

        public void DivideBy(string name, decimal value)
        {
            if (variables.Where(v => v.GetName().Equals(name)).Count() == 1)
            {
                Named nv = variables.First(v => v.GetName().Equals(name));
                if (nv is NumericVariable)
                    (nv as NumericVariable).DivideBy(value);
            }
        }

    }
}
