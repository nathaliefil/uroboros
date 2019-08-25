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
using Uroboros.syntax.lexer;
using Uroboros.syntax.runtime;
using Uroboros.syntax.commands.core;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.interpretation;
using Uroboros.syntax.commands.structures;
using Uroboros.syntax.structures.abstracts;
using Uroboros.syntax.structures;

namespace Uroboros.gui
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            CodeBoxSettings();
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
                if (codeBox.Text.Trim().Equals(""))
                {
                    Log("ERROR! No command found.");
                }
                else
                {
                    string code = codeBox.Text;
                    string location = locationBox.Text;
                    Runner.Run(code, location);
                }
            }
            Log("------------------------------------");
        }

        private void Log(string text)
        {
            Logger.GetInstance().Log(text);
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
    }
}
