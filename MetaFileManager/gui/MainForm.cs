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
using System.Reflection;
using Uroboros.syntax.log;

namespace Uroboros.gui
{
    public partial class MainForm : Form
    {
        string version;

        public MainForm()
        {
            InitializeComponent();
            InitialSettings();

            Log("Welcome to Meta File Manager");
            Log("Uroboros ver. " + version);
            LogLine();
        }

        private void InitialSettings()
        {
            CodeBoxSettings();
            locationBox.Text = "";
            locationBox.TextAlign = HorizontalAlignment.Right;
            logBox.ScrollBars = ScrollBars.Vertical;
            Logger.GetInstance().SetOutputBox(logBox);
            version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (locationBox.Text.Equals(""))
            {
                LogSyntaxError("ERROR! Location not found.");
            }
            else
            {
                if (codeBox.Text.Trim().Equals(""))
                {
                    LogSyntaxError("ERROR! No command found.");
                }
                else
                {
                    string code = codeBox.Text;
                    string location = locationBox.Text;
                    Runner.Run(code, location);
                }
            }
            LogLine();
        }

        private void Log(string text)
        {
            Logger.GetInstance().Log(text);
        }

        private void LogLine()
        {
            Logger.GetInstance().LogLine();
        }

        private void LogSyntaxError(string text)
        {
            Logger.GetInstance().LogSyntaxError(text);
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
