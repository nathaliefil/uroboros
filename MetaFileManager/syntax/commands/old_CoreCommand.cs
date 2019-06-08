using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DivineScript.syntax.commands
{
    partial class old_CoreCommand
    {
        /*private CommandType ctype;
        private ElementsType etype;
        private List<Subcommand> subcommands;
        private string[] files;
        private string[] catalogs;
        private int accessDenied;
        private int totalElements;

        public CoreCommand(CommandType ctype, ElementsType etype)
        {
            this.ctype = ctype;
            this.etype = etype;
            subcommands = new List<Subcommand>();
            accessDenied = 0;
        }

        public void AddSubcommand(Subcommand sbcom)
        {
            subcommands.Add(sbcom);
        }

        public int GetAccessDeniedCount()
        {
            return accessDenied;
        }

        public void BuildCoreCommand(List<string> corecom)
        {
        }

        public void PrepareElementsList(string directory)
        {
            switch (etype)
            {
                case ElementsType.Files:
                    {
                        catalogs = new string[0];
                        files = Directory.GetFiles(directory);
                        break;
                    }
                case ElementsType.Catalogs:
                    {
                        catalogs = Directory.GetDirectories(directory);
                        files = new string[0];
                        break;
                    }
                case ElementsType.All:
                    {
                        catalogs = Directory.GetDirectories(directory);
                        files = Directory.GetFiles(directory);
                        break;
                    }
                case ElementsType.Deepfiles:
                    {
                        catalogs = new string[0];
                        files = AddFilesFromCatalogRecursive(directory).ToArray();
                        break;
                    }
                case ElementsType.Deepcatalogs:
                    {
                        files = new string[0];
                        catalogs = AddCatalogsFromCatalogRecursive(directory).ToArray();
                        break;
                    }
            }
            totalElements = catalogs.Length + files.Length;

            // here go filthers - condition where, first, max...
        }

        private List<String> AddFilesFromCatalogRecursive(string location)
        {
            List<String> files = new List<String>();
            files.AddRange(Directory.GetFiles(location));

            foreach (string direct in Directory.GetDirectories(location))
            {
                files.AddRange(AddFilesFromCatalogRecursive(direct));
            }
            return files;
        }

        private List<String> AddCatalogsFromCatalogRecursive(string location)
        {
            List<String> files = new List<String>();
            files.AddRange(Directory.GetDirectories(location));

            foreach (string direct in Directory.GetDirectories(location))
            {
                files.AddRange(AddCatalogsFromCatalogRecursive(direct));
            }
            return files;
        }


        public int GetElementsNumber()
        {
            return catalogs.Length + files.Length;
        }

        public string[] GetElementsToLog(string baseDirectory)
        {
            string[] logElements = new string[catalogs.Length + files.Length];
            int beginning = baseDirectory.Length;
            for (int i = 0; i < catalogs.Length; i++)
            {
                logElements[i] = catalogs[i].Substring(beginning);
            }
            for (int i = 0; i < files.Length; i++)
            {
                logElements[i + catalogs.Length] = files[i].Substring(beginning);
            }
            return logElements;
        }

        public int GetTotalElementsNumber()
        {
            return totalElements;
        }

        public ElementsType GetEtype()
        {
            return etype;
        }

        public CommandType GetCtype()
        {
            return ctype;
        }

        private void FiltherElementsBySubcommands()
        {
            for (int i=0; i<subcommands.Count; i++)
            {
                FiltherBySubcommand(i);
            }
        }

        public void Run()
        {
            FiltherElementsBySubcommands();

            //here will be main part of the program
            //get ready

            switch (ctype)
            {
                case CommandType.Copy:
                    {
                        Copy();
                        break;
                    }
                case CommandType.CopyTo:
                    {
                        CopyTo();
                        break;
                    }
                case CommandType.Create:
                    {
                        Create();
                        break;
                    }
                case CommandType.Cut:
                    {
                        Cut();
                        break;
                    }
                case CommandType.CutTo:
                    {
                        CutTo();
                        break;
                    }
                case CommandType.Delete:
                    {
                        Delete();
                        break;
                    }
                case CommandType.Drop:
                    {
                        Drop();
                        break;
                    }
                case CommandType.MoveTo:
                    {
                        MoveTo();
                        break;
                    }
                case CommandType.RenameTo:
                    {
                        RenameTo();
                        break;
                    }
                case CommandType.Select:
                    {
                        Select();
                        break;
                    }
                case CommandType.Show:
                    {
                        Show();
                        break;
                    }
            }
        }

        private void Copy()
        {
        }

        private void CopyTo()
        {
        }

        private void Cut()
        {
        }

        private void CutTo()
        {
        }

        private void Create()
        {
        }

        private void Delete()
        {
            foreach (string file in files)
            {
                //FileSystem.DeleteFile(el, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            }
        }

        private void Drop()
        {
        }

        private void MoveTo()
        {
        }

        private void RenameTo()
        {
        }

        private void Select()
        {
        }

        private void Show()
        {
        }





        */

    }
}
