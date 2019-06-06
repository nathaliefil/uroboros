using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.commands
{
    partial class CoreCommand
    {
        private void FiltherBySubcommand(int index)
        {
            switch (subcommands[index].GetSType())
            {
                case SubcommandType.Where:
                    {
                        Where(index);
                        break;
                    }
                case SubcommandType.WhereNot:
                    {
                        WhereNot(index);
                        break;
                    }
                case SubcommandType.First:
                    {
                        First(index);
                        break;
                    }
                case SubcommandType.Last:
                    {
                        Last(index);
                        break;
                    }
                case SubcommandType.FirstPart:
                    {
                        FirstPart(index);
                        break;
                    }
                case SubcommandType.LastPart:
                    {
                        LastPart(index);
                        break;
                    }
                case SubcommandType.Ignore:
                    {
                        Ignore(index);
                        break;
                    }
                case SubcommandType.Max:
                    {
                        Max(index);
                        break;
                    }
                case SubcommandType.Stop:
                    {
                        Stop(index);
                        break;
                    }
                case SubcommandType.StopNot:
                    {
                        StopNot(index);
                        break;
                    }
                case SubcommandType.Order:
                    {
                        Order(index);
                        break;
                    }
                case SubcommandType.Each:
                    {
                        Each(index);
                        break;
                    }
            }
        }

        private void Where(int index)
        {
        }
        private void WhereNot(int index)
        {
        }
        private void First(int index)
        {
            int number = subcommands[index].GetIntegerVariable();
            if (number < 0)
            {
                number = 0;
            }

            if (number <= catalogs.Length)
            {
                catalogs = catalogs.Take(number).ToArray();
                files = new String[0];
            }
            else
            {
                number -= catalogs.Length;
                if (number <= files.Length)
                {
                    files = files.Take(number).ToArray();
                }
            }
        }
        private void FirstPart(int index)
        {

        }
        private void Last(int index)
        {
            int number = subcommands[index].GetIntegerVariable();
            if (number < 0)
            {
                number = 0;
            }

            if (number <= files.Length)
            {
                catalogs = new String[0];
                files = files.Skip(files.Length-number).ToArray(); ;
            }
            else
            {
                number -= files.Length;
                if (number <= catalogs.Length)
                {
                    catalogs = catalogs.Skip(catalogs.Length - number).ToArray(); ;
                }
            }
        }
        private void LastPart(int index)
        {

        }
        private void Ignore(int index)
        {
            int number = subcommands[index].GetIntegerVariable();
            if (number < 0)
            {
                number = 0;
            }

            if (number <= catalogs.Length)
            {
                catalogs = catalogs.Skip(number).ToArray();
            }
            else
            {
                number -= catalogs.Length;
                catalogs = new String[0];
                if (number <= files.Length)
                {
                    files = files.Skip(number).ToArray();
                }
                else
                    files = new String[0];
            }
        }
        private void Max(int index)
        {
        }
        private void Stop(int index)
        {
        }
        private void StopNot(int index)
        {

        }
        private void Order(int index)
        {
        }
        private void Each(int index)
        {
            int number = subcommands[index].GetIntegerVariable();
            if (number < 1)
            {
                number = 1;
            }

            int n_catalogs = (-1 + catalogs.Count() + number) / number;
            string[] newcatalogs = new string[n_catalogs];
            for (int i = 0; i < n_catalogs; i++)
            {
                newcatalogs[i] = catalogs[i * number];
            }


            catalogs = newcatalogs;
            
        }
    }
}
