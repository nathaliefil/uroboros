using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncFactorial : DefaultNumerable
    {
        private INumerable arg0;

        public FuncFactorial(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            decimal value = arg0.ToNumber();

            if (value < 0)
                throw new RuntimeException("RUNTIME ERROR! Factorial of negative number happened.");

            if (value % 1 == 0)
                return ArrayedFactorials((int)value);
            else
                return StirlingApproximation((double)value);
        }

        private decimal StirlingApproximation(double n)
        {
            return (decimal)(Math.Sqrt(2 * Math.PI * n) * Math.Pow(n / Math.E, n));
        }

        private decimal ArrayedFactorials(int n)
        {
            switch (n)
            {
                case 0:
                    return 1M;
                case 1:
                    return 1M;
                case 2:
                    return 2M;
                case 3:
                    return 6M;
                case 4:
                    return 24M;
                case 5:
                    return 120M;
                case 6:
                    return 720M;
                case 7:
                    return 5040M;
                case 8:
                    return 40320M;
                case 9:
                    return 362880M;
                case 10:
                    return 3628800M;
                case 11:
                    return 39916800M;
                case 12:
                    return 479001600M;
                case 13:
                    return 6227020800M;
                case 14:
                    return 87178291200M;
                case 15:
                    return 1307674368000M;
                case 16:
                    return 20922789888000M;
                case 17:
                    return 355687428096000M;
                case 18:
                    return 6402373705728000M;
                case 19:
                    return 121645100408832000M;
                case 20:
                    return 2432902008176640000M;
                case 21:
                    return 51090942171709440000M;
                case 22:
                    return 1124000727777607680000M;
                case 23:
                    return 25852016738884976640000M;
                case 24:
                    return 620448401733239439360000M;
                case 25:
                    return 15511210043330985984000000M;
                case 26:
                    return 403291461126605635584000000M;
                case 27:
                    return 10888869450418352160768000000M;
                default:
                    return Decimal.MaxValue;
            }
        }
    }
}
