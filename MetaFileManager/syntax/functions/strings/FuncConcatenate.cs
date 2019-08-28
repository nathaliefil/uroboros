using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.strings
{
    class FuncConcatenate : DefaultStringable
    {
        private IListable arg0;

        public FuncConcatenate(IListable arg0)
        {
            this.arg0 = arg0;
        }

        public override string ToString()
        {
            List<string> list = arg0.ToList();

            if (list.Count == 0)
                return "";
            else if (list.Count == 1)
                return list[0];

            StringBuilder sb = new StringBuilder();
            foreach (string l in list)
                sb.Append(l);

            return sb.ToString();
        }
    }
}
