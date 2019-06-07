using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MetaFileManager
{
    public class FileStringVariable : Stringable
    {
        private FileStringVariableType type;

        FileStringVariable(FileStringVariableType type)
        {
            this.type = type;
        }

        public FileStringVariableType GetVariableType()
        {
            return type;
        }


        public string ToString(string address, int index, int count)
        {
            switch (type)
            {
                case FileStringVariableType.BigIndex:
                    {
                        return new String('0', NumberOfDigits(count) - NumberOfDigits(index)) + Convert.ToString(index);
                    }

                case FileStringVariableType.BinaryIndex:
                    {
                        return Convert.ToString(index, 2);
                    }
                case FileStringVariableType.HexIndex:
                    {
                        List<int> ints = new List<int>();
                        do
                        {
                            ints.Add(index % 16);
                            index = (index - index % 16) / 16;
                        } while (index > 16);
                        ints.Reverse();
                        StringBuilder stringbuild = new StringBuilder();
                        foreach (int number in ints)
                        {
                            if(number<10)
                                stringbuild.Append(Convert.ToString(number));
                            else
                                stringbuild.Append(Convert.ToChar(55 + number));
                        }
                        return stringbuild.ToString();
                    }
                case FileStringVariableType.LetterIndex:
                    {
                        List<int> ints = new List<int>();
                        do
                        {
                            ints.Add(index % 26);
                            index = (index - index % 26)/26;
                        } while (index > 26);
                        ints.Reverse();
                        StringBuilder stringbuild = new StringBuilder();
                        foreach (int number in ints)
                        {
                            stringbuild.Append(Convert.ToChar(65+number));
                        }
                        return stringbuild.ToString();
                    }
                case FileStringVariableType.Name:
                    {
                        return Path.GetFileNameWithoutExtension(address);
                    }
                case FileStringVariableType.Fullname:
                    {
                        return Path.GetFileName(address);
                    }
                case FileStringVariableType.Extension:
                    {
                        return (Path.GetExtension(address)).Substring(1);
                    }
                case FileStringVariableType.CreationDate:
                    {
                        return " "; //todo
                    }
                case FileStringVariableType.ModificationDate:
                    {
                        return " "; //todo
                    }

                // todo for every type
            }


            return " ";
        }

        public static int NumberOfDigits(int n)
        {
            // this method looks very ugly,
            // but is the most efficient by time of computation
            if (n < 10) return 1;
            if (n < 100) return 2;
            if (n < 1000) return 3;
            if (n < 10000) return 4;
            if (n < 100000) return 5;
            if (n < 1000000) return 6;
            if (n < 10000000) return 7;
            if (n < 100000000) return 8;
            if (n < 1000000000) return 9;
            return 10;
        }
    }
}
