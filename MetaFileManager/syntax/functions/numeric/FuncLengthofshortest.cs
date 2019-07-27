using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncLengthofshortest : DefaultNumerable
    {
        private IListable arg0;

        public FuncLengthofshortest(IListable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            List<string> list = arg0.ToList().OrderBy(x => x.Length).ToList();

            if (list.Count == 0)
                return 0;

            return list[0].Length;
        }
    }
}
