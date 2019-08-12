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
using Uroboros.syntax.interpretation;
using Uroboros.syntax.commands.structures;
using Uroboros.syntax.structures.abstracts;
using Uroboros.syntax.structures;

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
            List<Structure> structures = new List<Structure>();
            bool executing = true;

            try
            {
                int pointer = 0;

                while (pointer < commands.Count())
                {
                    if (executing)
                    {
                        ICommand takenCommand = commands[pointer];

                        if (takenCommand is BracketOn)
                        {
                            if (takenCommand is EmptyOpenning)
                                structures.Add(new EmptyBlock());
                            else if (takenCommand is IfOpenning)
                            {
                                if (!(takenCommand as IfOpenning).ToBool())
                                    executing = false;
                                structures.Add(new If());
                            }
                            else if (takenCommand is WhileOpenning)
                            {
                                if (!(takenCommand as WhileOpenning).ToBool())
                                    executing = false;
                                structures.Add(new While((takenCommand as WhileOpenning).GetCondition(), (takenCommand as Structure).GetCommandNumber()));
                            }
                            else if (takenCommand is InsideOpenning)
                            {
                                // todo
                            }
                            else if (takenCommand is ListLoopOpenning)
                            {
                                List<string> list = (takenCommand as ListLoopOpenning).ToList();
                                if (list.Count == 0)
                                {
                                    structures.Add(new EmptyBlock());
                                    executing = false;
                                }
                                else
                                {
                                    string value = list[0];
                                    list.RemoveAt(0);
                                    structures.Add(new ListLoop(list, (takenCommand as BracketOn).GetCommandNumber()));

                                    RuntimeVariables.GetInstance().Actualize("this", value);
                                    RuntimeVariables.GetInstance().Actualize("index", 0);
                                }
                            }
                            else if (takenCommand is NumericLoopOpenning)
                            {
                                int repeats = (int)(takenCommand as NumericLoopOpenning).ToNumber();
                                if (repeats <= 0)
                                {
                                    structures.Add(new EmptyBlock());
                                    executing = false;
                                }
                                else
                                {
                                    repeats--;
                                    structures.Add(new NumericLoop(repeats, (takenCommand as BracketOn).GetCommandNumber()));

                                    RuntimeVariables.GetInstance().Actualize("index", 0);
                                }
                            }
                        }
                        else if (takenCommand is BracketOff)
                        {
                            if (structures.Count == 0)
                                throw new RuntimeException("ERROR! Brackets are wrong.");

                            Structure lastStructure = structures.Last();

                            if (lastStructure is ILoopingStructure)
                            {
                                bool iterateOneMoreTime = lastStructure.HasNext();

                                if (iterateOneMoreTime)
                                    pointer = lastStructure.GetCommandNumber();
                                else
                                    structures.RemoveAt(structures.Count - 1);
                            }
                            

                        }
                        else
                        {

                            commands[pointer].Run();
                        }
                    }
                    else
                    {
                    }




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
