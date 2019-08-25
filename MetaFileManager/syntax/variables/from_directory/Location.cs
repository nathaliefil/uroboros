using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.variables
{
    class Location : StringVariable
    {

        public Location() 
            : base ("", "")
        {
            name = "location";
            value = "";
        }

        public override void SetValue(string str)
        {
            value = String.Copy(str);
            RuntimeVariables.GetInstance().ClearPath();
        }
    }
}
