using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DivineScript.syntax;
using DivineScript.syntax.commands;
using DivineScript.syntax.reading;
using DivineScript.syntax.runtime;
using DivineScript.syntax.commands.core;
using DivineScript.syntax.variables.expressions;

namespace DivineScript
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
            Log("--------------------------");
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (locationBox.Text.Equals(""))
            {
                Log("Error! Location not found.");
            }
            else
            {
                if (codeBox.Text.Length == 0)
                {
                    Log("Error! No command found.");
                }
                else
                {
                    try
                    {
                        RuntimeVariables.GetInstance().InitializeInnerVariables();
                        RuntimeVariables.GetInstance().Actualize("location", locationBox.Text);
                        List<Token> tokens = Reader.CreateTokenlist(codeBox.Text);
                        commands = CommandListFactory.Build(tokens);
                        //




                        List<string> files = new List<string>();
                        files.Add("3");
                        files.Add("info5.txt");
                        files.Add("info.txt");
                        files.Add("33");
                        Copy comopen = new Copy(new ListExpression(files));
                        commands.Add(comopen);



                        //
                        try
                        {
                            foreach (ICommand command in commands)
                            {
                                command.Run();
                            }
                        }
                        catch (DivineScript.syntax.RuntimeException re)
                        {
                            Logger.GetInstance().Log(re.GetMessage());
                        }
                    }
                    catch (DivineScript.syntax.SyntaxErrorException te)
                    {
                        Logger.GetInstance().Log(te.GetMessage());
                    }/*
                    catch (NullReferenceException nre)
                    {
                        Logger.GetInstance().Log("Error! NullReferenceException");
                    }*/
                }
            }
            Log("--------------------------");
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
