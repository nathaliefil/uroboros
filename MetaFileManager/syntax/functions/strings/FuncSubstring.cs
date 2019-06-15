﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.functions.strings.abstracts;
using DivineScript.syntax.variables.expressions;

namespace DivineScript.syntax.functions.numeric
{
    class FuncSubstring : IStringFunction
    {
        private StringExpression arg0;
        private NumericExpression arg1;
        private NumericExpression arg2;
        bool twoArgs;

        public FuncSubstring(StringExpression arg0, NumericExpression arg1, NumericExpression arg2)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
            twoArgs = false;
        }

        public FuncSubstring(StringExpression arg0, NumericExpression arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            twoArgs = true;
        }

        public override string ToString()
        {
            string source = arg0.ToString();
            int arg1v = (int)arg1.ToNumber();

            if (arg1v >= source.Length)
            {
                return "";
            }

            if (twoArgs)
            {
                return source.Substring(arg1v);
            }
            else
            {
                int arg2v = (int)arg1.ToNumber();
                if (arg2v < 1)
                {
                    return "";
                }
                if (arg1v + arg2v > source.Length)
                {
                    arg2v -= arg1v + arg2v - source.Length;
                }
                return source.Substring(arg1v, arg2v);
            }
        }
    }
}