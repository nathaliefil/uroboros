using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaFileManager
{
    public interface Numberable
    {
        bool isInteger { get; set; }

        int ToInt(string address, int index, int count);
        double ToDouble(string address, int index, int count);
    }
}
