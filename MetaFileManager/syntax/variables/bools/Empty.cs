using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.runtime;
using System.IO;

namespace DivineScript.syntax.variables.bools
{
    class Empty : NamedVariable, IBoolable
    {
        public Empty()
        {
            name = "empty";
        }

        public bool ToBool()
        {
            string thiss = RuntimeVariables.GetInstance().GetValueString("this");
            string location = RuntimeVariables.GetInstance().GetValueString("location") + "//" + thiss;

            if (FileValidator.IsDirectory(thiss))
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


        public decimal ToNumber()
        {
            return ToBool()? 1 : 0;
        }

        public override string ToString()
        {
            return ToBool() ? "1" : "0";
        }
    }
}
