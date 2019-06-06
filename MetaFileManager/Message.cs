using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DivineScript
{
    partial class Message
    {
        public static void showMessage(int id, int number) 
        {
            switch (id)
            {
                case -1:
                {
                    MessageBox.Show("Destination directory not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -2:
                {
                    MessageBox.Show("No command found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -3:
                {
                    MessageBox.Show("There is something wrong with brackets in command" + (number+1) + ".", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -4:
                {
                    MessageBox.Show("There is something wrong with quotation marks" + (number+1) + ".", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -5:
                {
                    MessageBox.Show("Command "+(number+1)+" is too short.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -6:
                {
                    MessageBox.Show("First word in command " + (number + 1) + " is not understood.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -7:
                {
                    MessageBox.Show("In command " + (number + 1) + " instruction 'move' has to indicate target location. Use word 'to' as third word.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -8:
                {
                    MessageBox.Show("In command " + (number + 1) + " instruction 'rename' has to indicate new names. Use word 'to' as third word.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -9:
                {
                    MessageBox.Show("In command " + (number + 1) + " instruction 'create' can only create new catalogs. Use word 'catalogs' as third word.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -10:
                {
                    MessageBox.Show("In command " + (number + 1) + " three words are too few for instruction 'create'. You have missed something.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -11:
                {
                    MessageBox.Show("In command " + (number + 1) + " there is something wrong with number of catalogs to create. Check second word.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -12:
                {
                    MessageBox.Show("In command " + (number + 1) + " type of elements to manipulate is not indentified. Check second word.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -13:
                {
                    MessageBox.Show("In command " + (number + 1) + " subcommand is not identified.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -14:
                {
                    MessageBox.Show("In command " + (number + 1) + " subcommand is too short.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                case -15:
                {
                    MessageBox.Show("In command " + (number + 1) + " there is something wrond with subcommand syntax.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }
        }
    }
}
