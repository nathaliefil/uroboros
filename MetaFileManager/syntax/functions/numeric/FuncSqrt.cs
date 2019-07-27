using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.functions.numeric
{
    class FuncSqrt : DefaultNumerable
    {
        private INumerable arg0;

        public FuncSqrt(INumerable arg0)
        {
            this.arg0 = arg0;
        }

        public override decimal ToNumber()
        {
            decimal number = arg0.ToNumber();

            if (number < 0)
                throw new RuntimeException("RUNTIME ERROR! Square root of negative number happened.");
            else if (number == 0)
                return 0;
            else
            {
                if (number % 1 == 0 && IsPerfectSquare((int)number))
                    return (decimal)Math.Sqrt((int)number);
                else
                    return SquareRoot(number);
            }
        }

        // nice stolen method
        static decimal SquareRoot(decimal square)
        {
            decimal root = square / 3;
            int i;
            for (i = 0; i < 32; i++)
                root = (root + square / root) / 2;
            return root;
        }

        static bool IsPerfectSquare(int number)
        {
            int root = (int)Math.Sqrt(number);
            return (int)Math.Pow(root, 2) == number;
        }
    }
}
