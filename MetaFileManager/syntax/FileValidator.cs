using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax
{
    class FileValidator
    {
        private static char[] notAllowedSigns = new char[] 
                {'\\', ':', '&', '*', '"', '<', '>'};


        public static bool IsNameCorrect(string name)
        {
            return name.IndexOfAny(notAllowedSigns) >= 0 ? false : true;
        }
        public static bool IsDirectory(string name)
        {
            return !name.Contains('.');
        }
    }
}
