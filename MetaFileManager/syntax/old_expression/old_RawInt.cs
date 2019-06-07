using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaFileManager
{
    public class RawInt : Numberable, Stringable
    {
        private int content;
        public bool isInteger { get; set; }

        RawInt(int content)
        {
            this.content = content;
            this.isInteger = true;
        }

        public int ToInt(string address, int index, int count)
        {
            return content;
        }

        public double ToDouble(string address, int index, int count)
        {
            return Convert.ToDouble(content);
        }

        public string ToString(string address, int index, int count)
        {
            return Convert.ToString(content);
        }
    }
}
