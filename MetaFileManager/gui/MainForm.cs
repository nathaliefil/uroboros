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
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.interpretation.vars_range;

namespace Uroboros.gui
{
    public partial class MainForm : Form
    {
        private const int MIN_WINDOW_WIDTH = 600;
        private const int MIN_WINDOW_HEIGHT = 200;

        List<ICommand> commands;
        List<Token> tokens;

        public MainForm()
        {
            InitializeComponent();
            locationBox.Text = "";
            locationBox.TextAlign = HorizontalAlignment.Right;
            logBox.ScrollBars = ScrollBars.Vertical;
            Logger.GetInstance().SetOutputBox(logBox);


            Log("Welcome to Meta File Manager");
            Log("Uroboros version: alpha");
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
                    Run();
                }
            }
            Log("------------------------------------");
            //System.GC.Collect();
        }

        private void Run()
        {
            Logger.GetInstance().LogOn();
            InterVariables.GetInstance().Clear();
            RuntimeVariables.GetInstance().InitializeInnerVariables();
            RuntimeVariables.GetInstance().Actualize("location", locationBox.Text);

            try
            {
                List<Token> new_tokens = Reader.CreateTokenlist(codeBox.Text);
                if (ListsAreDifferent(new_tokens, tokens))
                {
                    tokens = new_tokens;
                    commands = CommandListFactory.Build(new_tokens);
                }
                RunCode();
            }
            catch (Uroboros.syntax.SyntaxErrorException te)
            {
                Log(te.GetMessage());
            }


        }

        private void RunCode()
        {
            try
            {
                int pointer = 0;

                while (pointer < commands.Count())
                {
                    commands[pointer].Run();






                    pointer++;
                }
            }
            catch (Uroboros.syntax.RuntimeException re)
            {
                Log(re.GetMessage());
            }
        }

        private bool ListsAreDifferent(List<Token> tokens_1, List<Token> tokens_2)
        {
            //todo
            return true;
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

        private void codeBox_TextChanged(object sender, EventArgs e)
        {
            RefreshCodeBoxGraphics();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;

            codeBox.Height = 522 - 619 + control.Height;
            logBox.Height = 522 - 619 + control.Height;
            codeBox.Width = 730 - 1130 + control.Width;
            logBox.Left = 745 - 1130 + control.Width;
            locationBox.Width = 978 - 1130 + control.Width;
            directoryButton.Left = 1068 - 1130 + control.Width;

            // a lot of magic numbers
            /// to refactor

            if (this.Width < MIN_WINDOW_WIDTH)
                this.Width = MIN_WINDOW_WIDTH;

            if (this.Height < MIN_WINDOW_HEIGHT)
                this.Height = MIN_WINDOW_HEIGHT;
        }
    }
}
