using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaFileManager
{
    public class StringExpression : Stringable
    {
        private List<Stringable> elements;

        StringExpression()
        {
            elements = new List<Stringable>();
        }

        public void Add (Stringable element)
        {
            elements.Add(element);
        }

        public string ToString(string address, int index, int count)
        {
            StringBuilder stringbuild = new StringBuilder();
            foreach (Stringable element in elements)
            {
                stringbuild.Append(element.ToString());
            }
            return stringbuild.ToString() ;
        }
    }
}
