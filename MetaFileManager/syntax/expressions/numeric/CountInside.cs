using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.expressions.numeric
{
    class CountInside : DefaultNumerable
    {
        private IListable list;
        private IStringable directory;

        public CountInside(IListable list, IStringable directory)
        {
            this.list = list;
            this.directory = directory;
        }

        public override decimal ToNumber()
        {
            string d = directory.ToString();
            if (d.Trim().Equals(""))
                return 0;

            RuntimeVariables.GetInstance().ExpandLocation(d);

            if (!RuntimeVariables.GetInstance().WholeLocationExists())
            {
                RuntimeVariables.GetInstance().RetreatLocation();
                return 0;
            }

            decimal count = list.ToList().Count;
            RuntimeVariables.GetInstance().RetreatLocation();
            return count;
        }
    }
}
