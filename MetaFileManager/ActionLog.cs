using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.commands;

namespace DivineScript.syntax
{
    class ActionLog
    {
        /*public static string Build(CommandType ctype, ElementsType etype, int count, int maxcount)
        {
            string cstring;
            string estring;

            switch (etype)
            {
                case ElementsType.All:
                    {
                        estring = "files and catalogs";
                        break;
                    }
                case ElementsType.Files:
                    {
                        estring = "files";
                        break;
                    }
                case ElementsType.Catalogs:
                    {
                        estring = "catalogs";
                        break;
                    }
                case ElementsType.Deepfiles:
                    {
                        estring = "files";
                        break;
                    }
                case ElementsType.Deepcatalogs:
                    {
                        estring = "catalogs";
                        break;
                    }
                default:
                    {
                        estring = "elements";
                        break;
                    }
            }
            switch (ctype)
            {
                case CommandType.Print:
                    {
                        cstring = "have been printed";
                        break;
                    }
                case CommandType.Copy:
                    {
                        cstring = "have been copied to clipboard";
                        break;
                    }
                case CommandType.CopyTo:
                    {
                        cstring = "have been copied to new location";
                        break;
                    }
                case CommandType.Cut:
                    {
                        cstring = "have been cut to clipboard";
                        break;
                    }
                case CommandType.CutTo:
                    {
                        cstring = "have been cut to new location";
                        break;
                    }
                case CommandType.Create:
                    {
                        cstring = "have been created";
                        break;
                    }
                case CommandType.Delete:
                    {
                        cstring = "have been deleted";
                        break;
                    }
                case CommandType.Drop:
                    {
                        cstring = "have been dropped";
                        break;
                    }
                case CommandType.MoveTo:
                    {
                        cstring = "have been moved to new location";
                        break;
                    }
                case CommandType.RenameTo:
                    {
                        cstring = "have been renamed";
                        break;
                    }
                case CommandType.Select:
                    {
                        cstring = "have been selected";
                        break;
                    }
                case CommandType.Show:
                    {
                        cstring = "have been showed";
                        break;
                    }
                default:
                    {
                        cstring = " ";
                        break;
                    }
            }

            if (ctype == CommandType.Create)
            {
                return "      " + count + " " + estring + " " + cstring;
            }
            else
            {
                return "      " + count + " " + estring + " " + cstring + " (out of " + maxcount + ")";
            }
        }
        public static string BuildAccessDenied(ElementsType etype, int count)
        {
            string estring;

            switch (etype)
            {
                case ElementsType.All:
                    {
                        estring = "files and catalogs";
                        break;
                    }
                case ElementsType.Files:
                    {
                        estring = "files";
                        break;
                    }
                case ElementsType.Catalogs:
                    {
                        estring = "catalogs";
                        break;
                    }
                case ElementsType.Deepfiles:
                    {
                        estring = "files";
                        break;
                    }
                case ElementsType.Deepcatalogs:
                    {
                        estring = "catalogs";
                        break;
                    }
                default:
                    {
                        estring = "elements";
                        break;
                    }
            }

            return "      ^access denied for " + count + " " + estring;
        }*/
    }
}
