using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncProduct : DefaultNumerable
    {
        private List<INumerable> arg0;

        public FuncProduct(List<INumerable> arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            decimal result = arg0[0].ToNumber();

            foreach (INumerable inum in arg0.Skip(1))
            {
                if (result == 0)
                    return 0;

                result *= inum.ToNumber();
            }

            return result;
        }
    }
}
