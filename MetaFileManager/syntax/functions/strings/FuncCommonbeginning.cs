using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncCommonbeginning : DefaultStringable
    {
        private IListable arg0;

        public FuncCommonbeginning(IListable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            List<string> list = arg0.ToList().OrderBy(x => x).ToList();

            string first = list.First();
            string last = list.Last();
            int shortest = list.Min(y => y.Length);
            StringBuilder stringb = new StringBuilder();

            for (int i = 0; i < shortest; i++)
            {
                if (first[i].Equals(last[i]))
                    stringb.Append(first[i]);
                else
                    break;
            }
            return stringb.ToString();
        }
    }
}
