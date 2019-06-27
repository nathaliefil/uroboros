using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax
{
    public static class ExtensionMethods
    {
        public static bool IsNull(this object T)
        {
            return T == null;
        }
    }
}
