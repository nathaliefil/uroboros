using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

namespace Uroboros.gui
{
    partial class MainForm : Form
    {

        private const int MIN_WINDOW_WIDTH = 600;
        private const int MIN_WINDOW_HEIGHT = 200;

        Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);




        private void CodeBoxSettings()
        {
            codeBox.AcceptsTab = true;
            codeBox.AutoIndent = true;
        }

        private void codeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // paint comments
            Range range = (sender as FastColoredTextBox).VisibleRange;
            range.ClearStyle(GreenStyle);
            range.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            range.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            range.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline |
                        RegexOptions.RightToLeft);
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
