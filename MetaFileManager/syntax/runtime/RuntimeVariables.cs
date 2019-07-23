using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.variables.from_location;
using Uroboros.syntax.variables.bools;
using Uroboros.syntax.variables.from_location.date;

namespace Uroboros.syntax.runtime
{
    public partial class RuntimeVariables
    {
        private static RuntimeVariables INSTANCE = new RuntimeVariables();
        private List<Named> variables;

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
            foreach (Named var in variables)
            {
                var.BracketsUp();
            }
        }

        public void BracketsDown()
        {
            bool modified = false;
            List<Named> copy = new List<Named>(variables.ToArray());

            foreach (Named var in variables)
            {
                var.BracketsDown();
                if (var.NegativeDepth())
                {
                    copy.Remove(var);
                    if (modified == false)
                        modified = true;
                }
            }
            if (modified)
                variables = copy;
        }

        public List<string> GetValueList(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return new List<string>();

            Named nv = variables.First(v => name.Equals(v.GetName()));
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

            Named nv = variables.First(v => name.Equals(v.GetName()));
            return nv.ToString();
        }

        public decimal GetValueNumber(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return 0;

            Named nv = variables.First(v => name.Equals(v.GetName()));
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

            Named nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is IBoolable)
            {
                return (nv as IBoolable).ToBool();
            }
            return false;
        }

        public string GetListElement(string name, int index)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return "";

            Named nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is IListable)
            {
                List<string> lst = (nv as IListable).ToList();
                int count = lst.Count;

                if (index >= count || index < -count)
                    return "";

                if (index < 0)
                    index += count;

                return lst[index];
            }
            return "";
        }

        public void InitializeInnerVariables()
        {
            variables = new List<Named>();

            variables.Add(new Files());
            variables.Add(new Directories());
            variables.Add(new Everything());

            variables.Add(new StringVariable("this", ""));
            variables.Add(new StringVariable("location", ""));
            variables.Add(new NumericVariable("index", 0));

            variables.Add(new Name());
            variables.Add(new Fullname());
            variables.Add(new Exist());
            variables.Add(new Empty());
            variables.Add(new Extension());
            variables.Add(new Modification());
            variables.Add(new Creation());
            variables.Add(new Size());

            variables.Add(new DateVariableNumeric("modification.year", true, DateVariableType.Year));
            variables.Add(new DateVariableNumeric("modification.month", true, DateVariableType.Month));
            variables.Add(new DateVariableNumeric("modification.weekday", true, DateVariableType.WeekDay));
            variables.Add(new DateVariableNumeric("modification.day", true, DateVariableType.Day));
            variables.Add(new DateVariableNumeric("modification.hour", true, DateVariableType.Hour));
            variables.Add(new DateVariableNumeric("modification.minute", true, DateVariableType.Minute));
            variables.Add(new DateVariableNumeric("modification.second", true, DateVariableType.Second));
            variables.Add(new DateVariableString("modification.date", true, DateVariableType.Date));
            variables.Add(new DateVariableString("modification.clock", true, DateVariableType.Clock));

            variables.Add(new DateVariableNumeric("creation.year", false, DateVariableType.Year));
            variables.Add(new DateVariableNumeric("creation.month", false, DateVariableType.Month));
            variables.Add(new DateVariableNumeric("creation.weekday", false, DateVariableType.WeekDay));
            variables.Add(new DateVariableNumeric("creation.day", false, DateVariableType.Day));
            variables.Add(new DateVariableNumeric("creation.hour", false, DateVariableType.Hour));
            variables.Add(new DateVariableNumeric("creation.minute", false, DateVariableType.Minute));
            variables.Add(new DateVariableNumeric("creation.second", false, DateVariableType.Second));
            variables.Add(new DateVariableString("creation.date", false, DateVariableType.Date));
            variables.Add(new DateVariableString("creation.clock", false, DateVariableType.Clock));

        }

    }
}
