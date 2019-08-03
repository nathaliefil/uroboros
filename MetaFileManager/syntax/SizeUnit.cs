using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax
{
    class SizeUnit
    {
        public static decimal GetMultiplier(SizeSufix sufix)
        {
            //here are the constants
            switch (sufix)
            {
                case SizeSufix.None:
                    return 1M;
                case SizeSufix.KB:
                    return 1024M;
                case SizeSufix.MB:
                    return 1048576M;
                case SizeSufix.GB:
                    return 1073741824M;
                case SizeSufix.TB:
                    return 1125899906842624M;
                case SizeSufix.PB:
                    return 1152921504606846976M;
                case SizeSufix.K:
                    return 1000M;
                case SizeSufix.KK:
                    return 1000000M;
            }
            return 1M;
        }
    }
}
