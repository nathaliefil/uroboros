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

namespace DivineScript
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            locationBox.Text = "";
            locationBox.TextAlign = HorizontalAlignment.Right;
            codeBox.AcceptsTab = true;
            logBox.ScrollBars = ScrollBars.Vertical;

            Log("Welcome to Meta File Manager.");
            Log("--------------------------");
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            //Log("START PROGRAM");
            if (locationBox.Text.Equals(""))
            {
                Message.showMessage(-1,0);
            }
            else
            {
                if (codeBox.Text.Length == 0)
                {
                    Message.showMessage(-2,0);
                }
                else
                {
                    try
                    {
                        List<Token> tokens = Reader.CreateTokenlist(codeBox.Text);
                        foreach (Token t in tokens)
                        {
                            Log(t.Print());
                        }
                    }
                    catch(DivineScript.syntax.SyntaxErrorException te)
                    {
                        Log(te.GetMessage());
                    }
                    


                    // this

                    List<Programme> programs = new List<Programme>();
                    programs.Add(new Programme("main", codeBox.Text));

                    
                }
            }
            //Log("STOP PROGRAM");
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
            if (logBox.Text.Length == 0)
                logBox.AppendText(text);
            else
                logBox.AppendText(Environment.NewLine + text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
