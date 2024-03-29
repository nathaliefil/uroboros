﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.variables.from_file;
using Uroboros.syntax.variables.refers;

namespace Uroboros.syntax.runtime
{
    public partial class RuntimeVariables
    {
        private static RuntimeVariables INSTANCE = new RuntimeVariables();
        private List<Named> variables;
        private List<string> additionalLocationPath;

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

        public DateTime GetValueTime(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return DateTime.MinValue;

            Named nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is ITimeable)
            {
                return (nv as ITimeable).ToTime();
            }
            return DateTime.MinValue;
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

        public decimal GetTimeElement(string name, TimeVariableType type)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return 0;

            Named nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is ITimeable)
            {
                return (nv as ITimeable).ToTimeVariable(type);
            }
            return 0;
        }

        public string GetTimeDate(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return "";

            Named nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is ITimeable)
            {
                return (nv as ITimeable).ToDate();
            }
            return "";
        }

        public string GetTimeClock(string name)
        {
            if (variables.Where(v => name.Equals(v.GetName())).Count() == 0)
                return "";

            Named nv = variables.First(v => name.Equals(v.GetName()));
            if (nv is ITimeable)
            {
                return (nv as ITimeable).ToClock();
            }
            return "";
        }

        public void InitializeInnerVariables()
        {
            variables = new List<Named>();
            additionalLocationPath = new List<string>();

            variables.Add(new Files());
            variables.Add(new Directories());
            variables.Add(new Everything());

            variables.Add(new StringVariable("this", ""));
            variables.Add(new NumericVariable("index", 0));
            variables.Add(new Location());
            variables.Add(new WholeLocation());
            variables.Add(new Path());

            variables.Add(new Name());
            variables.Add(new Fullname());
            variables.Add(new Exist());
            variables.Add(new Empty());
            variables.Add(new Extension());
            variables.Add(new Access());
            variables.Add(new Modification());
            variables.Add(new Creation());
            variables.Add(new Now());
            variables.Add(new Size());
            variables.Add(new IsFile());
            variables.Add(new IsDirectory());
            variables.Add(new IsCorrect());
            variables.Add(new Success());
            variables.Add(new ReadOnly());
            variables.Add(new Hidden());
        }
    }
}
