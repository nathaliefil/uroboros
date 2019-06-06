using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaFileManager
{
    public class RawString : Stringable
    {
        private string content;

        RawString(string content)
        {
            this.content = content;
        }

        public string ToString(string address, int index, int count)
        {
            return content;
        }
    }
}
