using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaFileManager
{
    public class FileIntVariable : Numberable, Stringable
    {
        private FileIntVariableType type;
        public bool isInteger { get; set; }

        FileIntVariable(FileIntVariableType type)
        {
            this.type = type;
            this.isInteger = true;
        }

        public FileIntVariableType GetVariableType()
        {
            return type;
        }

        public double ToDouble(string address, int index, int count)
        {
            int value = ToInt(address, index, count);
            return Convert.ToDouble(value);
        }

        public string ToString(string address, int index, int count)
        {
            int value = ToInt(address, index, count);
            return Convert.ToString(value);
        }
        
        public int ToInt(string address, int index, int count)
        {
            switch (type)
            {
                case FileIntVariableType.Index:
                    {
                        return index;
                    }
                case FileIntVariableType.Count:
                    {
                        return count;
                    }
                // todo for every type
            }


            return 0;
        }

        
    }
}
