using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.functions
{
    class NumericFunctionTypes
    {
        private static string[] NUMERIC_FUNCTIONS = new string[] 
            { "ceil", "count", "e", "floor", "length", "max", "min", "number",
            "pi", "power", "round", "substring"};

        private static string[] STRING_FUNCTIONS = new string[] 
            { "binary", "digits", "filled", "hex", "letter", "lower", 
                "substring", "upper"};



        public static bool IsNumericFunction(string name)
        {
            return NUMERIC_FUNCTIONS.Contains(name);
        }

        public static bool IsStringFunction(string name)
        {
            return STRING_FUNCTIONS.Contains(name);
        }
    }
}
