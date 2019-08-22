using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.strings
{
    class FuncBeforeText : DefaultStringable
    {
        private IStringable arg0;
        private IStringable arg1;

        public FuncBeforeText(IStringable arg0, IStringable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override string ToString()
        {
            string text = arg0.ToString();
            string phrase = arg1.ToString();

            int index = text.IndexOf(phrase);
            if (index <= 0)
                return "";

            return text.Substring(0, index);
        }
    }
}
