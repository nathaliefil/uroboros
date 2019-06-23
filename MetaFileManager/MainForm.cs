using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Uroboros.syntax;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.runtime;
using Uroboros.syntax.commands.core;
using Uroboros.syntax.commands.create;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.interpretation.vars_range;

namespace Uroboros
{
    public partial class MainForm : Form
    {
        List<ICommand> commands;

        public MainForm()
        {
            InitializeComponent();
            locationBox.Text = "";
            locationBox.TextAlign = HorizontalAlignment.Right;
            codeBox.AcceptsTab = true;
            logBox.ScrollBars = ScrollBars.Vertical;
            Logger.GetInstance().SetOutputBox(logBox);


            Log("Welcome to Meta File Manager.");
            Log("------------------------------------");
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (locationBox.Text.Equals(""))
            {
                Log("ERROR! Location not found.");
            }
            else
            {
                if (codeBox.Text.Length == 0)
                {
                    Log("ERROR! No command found.");
                }
                else
                {
                    try
                    {
                        InterVariables.GetInstance().Clear();
                        RuntimeVariables.GetInstance().InitializeInnerVariables();
                        RuntimeVariables.GetInstance().Actualize("location", locationBox.Text);
                        List<Token> tokens = Reader.CreateTokenlist(codeBox.Text);

                        /*foreach (Token tk in tokens)
                        {
                            Log(tk.Print());
                        }*/


                        commands = CommandListFactory.Build(tokens);
                        //
                        /*
                        List<IStringable> lstrin= new List<IStringable>();
                        lstrin.Add(new StringConstant("info"));
                        StringExpression strin = new StringExpression(lstrin);

                        List<IStringable> lstrin2 = new List<IStringable>();
                        lstrin2.Add(new StringConstant("info_kopia"));
                        StringExpression strin2 = new StringExpression(lstrin2);


                        List<string> files = new List<string>();
                        files.Add("3");
                        files.Add("info5.txt");
                        files.Add("info.txt");
                        files.Add("33");


                        CreateDirectory comopen = new CreateDirectory(strin);
                        commands.Add(comopen);

                        */

                        //Cut com = new Cut(new StringConstant("info.txt"));
                        //commands.Add(com);

                        try
                        {
                            foreach (ICommand command in commands)
                            {
                                command.Run();
                            }
                        }
                        catch (Uroboros.syntax.RuntimeException re)
                        {
                            Log(re.GetMessage());
                        }
                    }
                    catch (Uroboros.syntax.SyntaxErrorException te)
                    {
                        Log(te.GetMessage());
                    }
                }
            }
            Log("------------------------------------");
        }

        private void directoryButton_Click(object sender, EventArgs e)
        {
            using (var fldrDlg = new FolderBrowserDialog())
            {
                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    locationBox.Text = fldrDlg.SelectedPath;
                }
            }
        }

        private void Log(string text)
        {
            Logger.GetInstance().Log(text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
