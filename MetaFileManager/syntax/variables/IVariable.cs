using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.variables
{
    interface IVariable
    {

        int ToInt();
        double ToDouble();
        string ToString();
        bool ToBool();

    }
}
