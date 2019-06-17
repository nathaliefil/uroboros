﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables;
using DivineScript.syntax.reading;
using DivineScript.syntax.interpretation.vars_range;
using DivineScript.syntax.variables.refers;

namespace DivineScript.syntax.interpretation.expressions
{
    class ListableBuilder
    {
        public static IListable Build(List<Token> tokens)
        {
            IStringable ist = StringableBuilder.Build(tokens);
            if (!(ist is NullVariable))
            {
                return ist;
            }

            

            //code

            return new NullVariable();
        }
    }
}
