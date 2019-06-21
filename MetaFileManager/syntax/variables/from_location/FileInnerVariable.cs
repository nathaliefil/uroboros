using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_location
{
    class FileInnerVariable
    {
        public static string GetName(string file)
        {
            int position = file.LastIndexOf("\\");
            if (position > -1)
                file = file.Substring(position);

            if (file.LastIndexOf('.') == -1)
                return file;
            else
                return file.Substring(0, file.LastIndexOf('.'));
        }

        public static string GetFullname(string file)
        {
            int position = file.LastIndexOf("\\");
            if (position > -1)
                file = file.Substring(position);
            return file;
        }

        public static string GetExtension(string file)
        {
            if (file.LastIndexOf('.') == -1)
                return "";
            else
                return file.Substring(file.LastIndexOf('.') + 1);
        }

        public static DateTime GetCreation(string file)
        {
            string address = RuntimeVariables.GetInstance().GetValueString("location") + "//" + file;

            try
            {
                if (FileValidator.IsDirectory(file))
                    return System.IO.Directory.GetCreationTime(@address);
                else
                    return System.IO.File.GetCreationTime(@address);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime GetModification(string file)
        {
            string address = RuntimeVariables.GetInstance().GetValueString("location") + "//" + file;

            try
            {
                if (FileValidator.IsDirectory(file))
                    return System.IO.Directory.GetLastWriteTime(@address);
                else
                    return System.IO.File.GetLastWriteTime(@address);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static decimal GetSize(string file)
        {
            string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + file;

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
