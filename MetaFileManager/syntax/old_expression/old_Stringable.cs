using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaFileManager
{
    public interface Stringable
    {
        string ToString (string address, int index, int count);
    }
}
