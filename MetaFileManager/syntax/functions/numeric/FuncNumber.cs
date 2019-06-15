﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.numeric.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.functions.numeric
{
    class FuncNumber : INumericFunction
    {
        private StringExpression arg0;

        public FuncNumber(StringExpression arg0)
        {
            this.arg0 = arg0;
        }

        public decimal ToNumber()
        {
            try
            {
                decimal value = Decimal.Parse(arg0.ToString());
                return value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override string ToString()
        {
            decimal value = ToNumber();

            if (value % 1 == 0)
                return ((int)value).ToString();
            else
                return value.ToString();
        }
    }
}