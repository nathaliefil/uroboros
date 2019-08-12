using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.runtime
{
    public partial class RuntimeVariables
    {

        public string GetLocation()
        {
            if (additionalLocationPath.Count == 0)
                return GetValueString("location");
            else
            {
                StringBuilder path = new StringBuilder(GetValueString("location"));
                foreach (string str in additionalLocationPath)
                {
                    path.Append("\\");
                    path.Append(str);
                }
                return path.ToString();
            }
        }

        public void ExpandLocation(string directory)
        {
            additionalLocationPath.Add(directory);
        }

        public void RetreatLocation()
        {
            if (additionalLocationPath.Count > 0)
                additionalLocationPath.RemoveAt(additionalLocationPath.Count - 1);
        }

        public void ReplaceLocationEnding(string directory)
        {
            int count = additionalLocationPath.Count;
            if (count > 0)
                additionalLocationPath[count - 1] = directory;
        }
    }
}
