using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using System.Collections;

namespace Uroboros.syntax.functions.numeric
{
    class FuncPower : DefaultNumerable
    {
        private INumerable arg0;
        private INumerable arg1;

        public FuncPower(INumerable arg0, INumerable arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }

        public override decimal ToNumber()
        {
            decimal basis = arg0.ToNumber();
            decimal power = arg1.ToNumber();


            if (power == 0)
                return 1;

            if (basis == 0)
                return 0;

            if (power == 1)
                return basis;

            if (IsInteger(power))
            {
                if (basis > 0)
                {
                    if (power > 0)
                        return PowerDecimalToInt(basis, (uint)power);
                    else
                        return 1 / PowerDecimalToInt(basis, (uint)-power);
                }
                else
                {
                    decimal sign = power % 2 == 0 ? 1M : -1M;

                    if (power > 0)
                        return sign * PowerDecimalToInt(-basis, (uint)power);
                    else
                        return sign / PowerDecimalToInt(-basis, (uint)-power);
                }
            }
            else
            {
                if (basis < 0)
                    throw new RuntimeException("RUNTIME ERROR! Exponentiation of a negative number resulted in complex number.");
                else
                    return (decimal)Math.Pow((double)basis, (double)power);
            }
        }

        public static bool IsInteger(decimal number)
        {
            return number == Math.Truncate(number);
        }

        public static decimal PowerDecimalToInt(decimal x, uint y)
        {
            decimal A = 1m;
            BitArray e = new BitArray(BitConverter.GetBytes(y));
            int t = e.Count;

            for (int i = t - 1; i >= 0; --i)
            {
                A *= A;
                if (e[i] == true)
                {
                    A *= x;
                }
            }
            return A;
        }
    }
}
