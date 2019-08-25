using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Uroboros.syntax.runtime
{
    public partial class RuntimeVariables
    {

        public string GetWholeLocation()
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

        public bool WholeLocationExists()
        {
            string location = GetWholeLocation();
            return Directory.Exists(@location);
        }

        public void ClearPath()
        {
            additionalLocationPath.Clear();
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

        public string GetPath()
        {
            if (additionalLocationPath.Count == 0)
                return "\\";
            else
            {
                StringBuilder path = new StringBuilder();
                foreach (string str in additionalLocationPath)
                {
                    path.Append("\\");
                    path.Append(str);
                }
                return path.ToString();
            }
        }
    }
}
