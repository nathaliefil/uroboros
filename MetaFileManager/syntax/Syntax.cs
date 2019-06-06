using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DivineScript.syntax.commands;

namespace DivineScript.syntax
{
    partial class Syntax
    {
        public static List<ICommand> GenerateCommands(string code){

            List<ICommand> commands = new List<ICommand>();



            /*
            string[] codes = code.Split(';');
            codes = codes.Where(x => !string.IsNullOrEmpty(x)).ToArray();*/

            /*for (int i = 0; i < codes.Length; i++ )
            {
                if (!CorrectBrackets(codes[i]))
                {
                    Message.showMessage(-3,i);
                    return new List<Command>();
                }
                if (!CorrectQuotations(codes[i]))
                {
                    Message.showMessage(-4,i);
                    return new List<Command>();
                }
                codes[i] = PrepareCode(codes[i]);
                string[] words = codes[i].Split(' ');
                words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (words.Count() <2 )
                {
                    Message.showMessage(-5,i);
                    return new List<Command>();
                }
                words[0] = words[0].ToLower();
                words[1] = words[1].ToLower();
                CommandType ctype;
                ElementsType etype;
                switch (words[0])
                {
                    case "print":
                        {
                            ctype = CommandType.Print;
                            break;
                        }
                    case "select":
                        {
                            ctype = CommandType.Select;
                            break;
                        }
                    case "show":
                        {
                            ctype = CommandType.Show;
                            break;
                        }
                    case "delete":
                        {
                            ctype = CommandType.Delete;
                            break;
                        }
                    case "drop":
                        {
                            ctype = CommandType.Drop;
                            break;
                        }
                    case "copy":
                        {
                            ctype = CommandType.Copy;
                            if (words.Count() > 2 && words[2].ToLower().Equals("to"))
                            {
                                ctype = CommandType.CopyTo;
                            }
                            break;
                        }
                    case "cut":
                        {
                            ctype = CommandType.Cut;
                            if (words.Count() > 2 && words[2].ToLower().Equals("to"))
                            {
                                ctype = CommandType.CutTo;
                            }
                            break;
                        }
                    case "move":
                        {
                            if (words.Count() > 2 && words[2].ToLower().Equals("to"))
                            {
                                ctype = CommandType.MoveTo;
                            }
                            else
                            {
                                Message.showMessage(-7, i);
                                return new List<Command>();
                            }
                            break;
                        }
                    case "rename":
                        {
                            if (words.Count() > 2 && words[2].ToLower().Equals("to"))
                            {
                                ctype = CommandType.RenameTo;
                            }
                            else
                            {
                                Message.showMessage(-8, i);
                                return new List<Command>();
                            }
                            break;
                        }
                    case "create":
                        {
                            if (words.Count() == 3)
                            {
                                Message.showMessage(-10, i);
                                return new List<Command>();
                            }
                            if (words.Count() > 2 && words[2].ToLower().Equals("catalogs"))
                            {
                                ctype = CommandType.Create;
                            }
                            else
                            {
                                Message.showMessage(-9, i);
                                return new List<Command>();
                            }
                            if (!words[1].All(char.IsDigit))
                            {
                                Message.showMessage(-11, i);
                                return new List<Command>();
                            }
                            break;
                        }
                    default:
                        {
                            Message.showMessage(-6, i);
                            return new List<Command>();
                        }
                }
                switch (words[1])
                {
                    case "files":
                        {
                            etype = ElementsType.Files;
                            break;
                        }
                    case "catalogs":
                        {
                            etype = ElementsType.Catalogs;
                            break;
                        }
                    case "all":
                        {
                            etype = ElementsType.All;
                            break;
                        }
                    case "*":
                        {
                            etype = ElementsType.All;
                            break;
                        }
                    case "deepfiles":
                        {
                            etype = ElementsType.Deepfiles;
                            break;
                        }
                    case "deepcatalogs":
                        {
                            etype = ElementsType.Deepcatalogs;
                            break;
                        }
                    default:
                        {
                            Message.showMessage(-12, i);
                            return new List<Command>();
                        }
                }


                Command command = new Command(ctype, etype);
                List<int> appearedKeywords = new List<int>();
                List<String> maincom = new List<String>();
                List<List<String>> subcoms = new List<List<string>>();

                bool subcommandsStarted = false;
                int subcomId = -1;

                for (int j = 0; j < words.Length; j++)
                {
                    if (IsAllowedSubcommandType(words[j]))
                        appearedKeywords.Add(j);
                }
                for (int j = 0; j < words.Length; j++)
                {
                    if (appearedKeywords.Contains(j))
                    {
                        subcomId++;
                        subcoms.Add(new List<string>());
                        subcommandsStarted = true;
                    }
                    if (subcommandsStarted)
                        subcoms[subcomId].Add(words[j]);
                    else
                        maincom.Add(words[j]);
                }
                command.BuildCoreCommand(maincom);


                foreach (List<String> subcom in subcoms)
                {
                    if (subcom.Count > 0 && IsAllowedSubcommandType(subcom[0].ToLower()))
                    {
                        if (subcom.Count == 1)
                        {
                            Message.showMessage(-14, i);
                            return new List<Command>();
                        }
                        else
                        {
                            SubcommandType sstype = GetSubcommandType(subcom[0].ToLower(), subcom[1].ToLower());
                            if (sstype == SubcommandType.NULL)
                            {
                                Message.showMessage(-13, i);
                                return new List<Command>();
                            }
                            if (!VerifySubcommand(sstype, subcom))
                            {
                                Message.showMessage(-15, i);
                                return new List<Command>();
                            }
                            Subcommand subcommand = new Subcommand(sstype, subcom);
                            command.AddSubcommand(subcommand);
                        }
                    }
                    else
                    {
                        Message.showMessage(-13, i);
                        return new List<Command>();
                    }
                }

                //MessageBox.Show(subcoms[1][1]);




                
                commands.Add(command);
                
            }*/
            return commands;
        }
    }
}
