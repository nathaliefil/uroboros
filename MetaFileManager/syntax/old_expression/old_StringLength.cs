using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaFileManager
{
    class StringLength: Numberable, Stringable
    {
        private Stringable content;
        public bool isInteger { get; set; }

        StringLength(Stringable content)
        {
            this.content = content;
            this.isInteger = true;
        }

        public double ToDouble(string address, int index, int count)
        {
            int value = ToInt(address, index, count);
            return Convert.ToDouble(value);
        }

        public string ToString(string address, int index, int count)
        {
            int value = ToInt(address, index, count);
            return Convert.ToString(value);
        }
        
        public int ToInt(string address, int index, int count)
        {
            return content.ToString().Length;
        }
    }
}
