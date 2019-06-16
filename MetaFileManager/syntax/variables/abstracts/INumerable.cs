﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.variables.abstracts
{
    interface INumerable : IStringable
    {
        decimal ToNumber();

    }
}
