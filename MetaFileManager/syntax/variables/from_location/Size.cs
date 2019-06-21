using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location
{
    class Size : NamedVariable, INumerable
    {
        public Size()
        {
            this.name = "size";
        }

        public decimal ToNumber()
        {
            string thiss = RuntimeVariables.GetInstance().GetValueString("this");
            string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + thiss;

            try
            {
                if (FileValidator.IsDirectory(location))
                    return (decimal)(DirSize(new DirectoryInfo(@location)));
                else
                    return (decimal)(new System.IO.FileInfo(location).Length);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return ToNumber().ToString();
        }

        private static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
