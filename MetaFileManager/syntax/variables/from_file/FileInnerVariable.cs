using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using System.IO;

namespace Uroboros.syntax.variables.from_file
{
    class FileInnerVariable
    {
        public static string GetName(string file)
        {
            if (file.Equals(""))
                return "";

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
            if (file.Equals(""))
                return "";

            int position = file.LastIndexOf("\\");
            if (position > -1)
                file = file.Substring(position);
            return file;
        }

        public static string GetExtension(string file)
        {
            if (file.Equals(""))
                return "";

            if (file.LastIndexOf('.') == -1)
                return "";
            else
                return file.Substring(file.LastIndexOf('.') + 1);
        }

        public static DateTime GetAccess(string file)
        {
            if (file.Equals(""))
                return DateTime.MinValue;

            string address = RuntimeVariables.GetInstance().GetWholeLocation() + "//" + file;

            try
            {
                if (FileValidator.IsDirectory(file))
                    return System.IO.Directory.GetLastAccessTime(@address);
                else
                    return System.IO.File.GetLastAccessTime(@address);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime GetCreation(string file)
        {
            if (file.Equals(""))
                return DateTime.MinValue;

            string address = RuntimeVariables.GetInstance().GetWholeLocation() + "//" + file;

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
            if (file.Equals(""))
                return DateTime.MinValue;

            string address = RuntimeVariables.GetInstance().GetWholeLocation() + "//" + file;

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
            if (file.Equals(""))
                return 0;

            string location = RuntimeVariables.GetInstance().GetWholeLocation() + "//" + file;

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

        public static bool Exist(string file)
        {
            if (file.Equals(""))
                return false;

            string location = RuntimeVariables.GetInstance().GetWholeLocation() +"//" + file;

            if (FileValidator.IsDirectory(file))
                return Directory.Exists(@location);
            else
                return File.Exists(@location);
        }

        public static bool Empty(string file)
        {
            // need to thing about thie method

            if (file.Equals(""))
                return true;

            string location = RuntimeVariables.GetInstance().GetWholeLocation() + "//" + file;

            if (FileValidator.IsDirectory(file))
            {
                if (Directory.Exists(@location))
                {
                    try
                    {
                        return Directory.EnumerateFileSystemEntries(location).Any() ? false : true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (File.Exists(@location))
                {
                    try
                    {
                        if (new FileInfo(location).Length == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool ExistInside(string file, string directory)
        {
            string directoryLocation = RuntimeVariables.GetInstance().GetWholeLocation() +"//" + directory;

            if (!Directory.Exists(@directoryLocation))
                return false;

            return Exist(directory + "//" + file);
        }
    }
}
