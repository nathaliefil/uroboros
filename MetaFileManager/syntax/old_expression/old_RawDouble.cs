using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaFileManager.syntax.expression
{
    public class RawDouble : Numberable, Stringable
    {
        private double content;
        public bool isInteger { get; set; }

        RawDouble(double content)
        {
            this.content = content;
            this.isInteger = false;
        }

        public int ToInt(string address, int index, int count)
        {
            return Convert.ToInt32(content);
        }

        public double ToDouble(string address, int index, int count)
        {
            return content;
        }

        public string ToString(string address, int index, int count)
        {
            return Convert.ToString(content);
        }
    }
}
