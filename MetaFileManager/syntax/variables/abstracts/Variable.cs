﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.variables.abstracts
{
    public abstract class Variable
    {
        private bool constant;

        public bool IsConstant()
        {
            return constant;
        }

    }
}
